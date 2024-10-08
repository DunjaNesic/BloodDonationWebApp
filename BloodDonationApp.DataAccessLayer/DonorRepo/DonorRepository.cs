﻿using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.DataAccessLayer.Extensions;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using Common.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.DonorRepo
{
    public class DonorRepository : RepositoryBase<Donor>, IDonorRepository
    {
        private readonly BloodDonationContext _context;
        public DonorRepository(BloodDonationContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<Donor> GetAllDonors(bool trackChanges, DonorParameters donorParameters)
        {
            var includes = new Expression<Func<Donor, object>>[]
          {
                 d => d.Place,
                 d => d.Place,
                 d => d.ListOfActions,
                 d => d.ListOfQuestionnaires,
                 d => d.CallsToDonate
          };            

            var query = GetAll(trackChanges, includes)
                .Filter(donorParameters.NextDonationDate, donorParameters.IsActive, donorParameters.BloodType, donorParameters.Sex, donorParameters.PlaceID)
                .Search(donorParameters.Search)
                .Sort(donorParameters.OrderBy)
                .Skip((donorParameters.PageNumber - 1) * donorParameters.PageSize)
                .Take(donorParameters.PageSize);  

            return query;
        }
        public IQueryable<Donor> GetDonorsByCondition(Expression<Func<Donor, bool>> condition, bool trackChanges)
        {
            var includes = new Expression<Func<Donor, object>>[]
          {
                 d => d.Place,
                 d => d.ListOfActions,
                 d => d.ListOfQuestionnaires,
                 d => d.CallsToDonate
          };

            var query = GetByCondition(condition, trackChanges, includes);
            return query;
        }
        public async Task<Donor?> GetByJMBG(string JMBG)
        {
            var donor = await _context.Donors
                .Include(d => d.Place).Include(d => d.CallsToDonate)
                .Where(d => d.JMBG.ToLower().Equals(JMBG.ToLower())).SingleOrDefaultAsync();

            return donor;
        }

        public async Task<IEnumerable<TransfusionAction>> GetActions(string jMBG)
        {
            var donor = await _context.Donors
                .Include(d => d.CallsToDonate)
                .FirstOrDefaultAsync(d => d.JMBG == jMBG);

            if (donor == null || donor.CallsToDonate == null || !donor.CallsToDonate.Any())
            {
                return Enumerable.Empty<TransfusionAction>();
            }

            var actionIds = donor.CallsToDonate.Select(ctd => ctd.ActionID).ToList();

            if (actionIds == null || !actionIds.Any())
            {
                return Enumerable.Empty<TransfusionAction>();
            }

            var actions = await _context.TransfusionActions
                .Include(a => a.Place)
                .Where(a => actionIds.Contains(a.ActionID))
                .ToListAsync();

            return actions;
        }

        public async Task<IEnumerable<TransfusionAction>> GetIncomingAction(string jMBG, bool forNotifications)
        {
            var donor = await _context.Donors
            .Include(d => d.CallsToDonate)
            .Include(d => d.Place)
            .FirstOrDefaultAsync(d => d.JMBG == jMBG);

            if (donor == null || donor.CallsToDonate == null || !donor.CallsToDonate.Any())
            {
                return Enumerable.Empty<TransfusionAction>();
            }

            IEnumerable<CallToDonate> calls;

            if (forNotifications)
            calls = donor.CallsToDonate.Where(ctd => ctd.AcceptedTheCall == false);

            else calls = donor.CallsToDonate.Where(ctd => ctd.AcceptedTheCall == true);
            

            var actionIds = calls.Select(ctd => ctd.ActionID).ToList();

            if (actionIds == null || !actionIds.Any())
            {
                return Enumerable.Empty<TransfusionAction>();
            }

            var cetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");

            var nowUtc = DateTime.UtcNow;
            var nowCET = TimeZoneInfo.ConvertTimeFromUtc(nowUtc, cetTimeZone);

            var actions = await _context.TransfusionActions
                .Include(a => a.Place)
                .Where(a => actionIds.Contains(a.ActionID) && a.ActionDate.Date >= nowCET.Date)
                .ToListAsync();


            return actions;
        }

        public async Task<IEnumerable<TransfusionAction>> GetDonorsHistory(string JMBG)
        {
            var donor = await _context.Donors
            .Include(d => d.CallsToDonate)
            .Include(d => d.Place)
            .FirstOrDefaultAsync(d => d.JMBG == JMBG);

            if (donor == null || donor.CallsToDonate == null || !donor.CallsToDonate.Any())
            {
                return Enumerable.Empty<TransfusionAction>();
            }


            var calls = donor.CallsToDonate.Where(ctd => ctd.ShowedUp == true);

            var actionIds = calls.Select(ctd => ctd.ActionID).ToList();

            if (actionIds == null || !actionIds.Any())
            {
                return Enumerable.Empty<TransfusionAction>();
            }

            var actions = await _context.TransfusionActions
                .Include(a => a.Place)
                .Include(a => a.ListOfQuestionnaires)
                .Where(a => actionIds.Contains(a.ActionID))
                .ToListAsync();

            return actions;
        }

        public async Task CreateDonor(Donor donor)
        {        
            await CreateAsync(donor);
        }

        public async Task<IEnumerable<Donor>> GetCalledDonorsAsync(int actionID, bool trackChanges)
        {
            var calledDonors = GetDonorsByCondition(
                d => d.CallsToDonate.Any(ctd => ctd.ActionID == actionID),
                trackChanges
            );

            return await calledDonors.ToListAsync();
        }

    }
}
