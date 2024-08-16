using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Action;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Donor;
using BloodDonationApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.DonorCallsRepo
{
    public class DonorCallsRepository : RepositoryBase<CallToDonate>, IDonorCallsRepository
    {
        private readonly BloodDonationContext _context;
        public DonorCallsRepository(BloodDonationContext context) : base(context)
        {
            _context = context;
        }

        public async Task CreateCall(string JMBG, int actionID, bool accepted)
        {
            CallToDonate call = new CallToDonate()
            {
                JMBG = JMBG,
                ActionID = actionID,
                AcceptedTheCall = accepted
            };
            await CreateAsync(call);
        }

        public async Task<object?> CreateCalls(string[] jMBGs, int actionID)
        {
            var action = await _context.TransfusionActions.FindAsync(actionID);
            if (action == null)
            {
                return null;
            }

            foreach (var jMBG in jMBGs)
            {
                var donor = await _context.Donors.FindAsync(jMBG);
                if (donor == null)
                {
                    return null;
                }

                var existingCall = await GetCall(jMBG, actionID, false);

                if (existingCall == null)
                {
                    var newCall = new CallToDonate
                    {
                        JMBG = jMBG,
                        ActionID = actionID,
                        AcceptedTheCall = false, 
                        ShowedUp = false 
                    };

                    _context.CallsToDonate.Add(newCall);
                }
            }

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<CallToDonate?> GetCall(string JMBG, int actionID, bool trackChanges)
        {
            var call = await GetByCondition((c => c.JMBG.Equals(JMBG) && c.ActionID == actionID), trackChanges).SingleOrDefaultAsync();
            return call;
        }

        public async Task<IEnumerable<CallToDonate?>> GetAACalls(string JMBG)
        {
            return await GetCallsByCriteria(JMBG, accepted: true, showedUp: true);
        }

        public async Task<IEnumerable<CallToDonate?>> GetADCalls(string JMBG)
        {
            return await GetCallsByCriteria(JMBG, accepted: true, showedUp: false);
        }

        public async Task<IEnumerable<CallToDonate?>> GetDDCalls(string JMBG)
        {
            return await GetCallsByCriteria(JMBG, accepted: false, showedUp: false);
        }

        public async Task<IEnumerable<CallToDonate?>> GetDACalls(string JMBG)
        {
            return await GetCallsByCriteria(JMBG, accepted: false, showedUp: true);
        }

        private async Task<IEnumerable<CallToDonate?>> GetCallsByCriteria(string JMBG, bool accepted, bool showedUp)
        {
            var donor = await _context.Donors
               .Include(d => d.CallsToDonate)
               .Include(d => d.Place)
               .FirstOrDefaultAsync(d => d.JMBG == JMBG);

            if (donor == null || donor.CallsToDonate == null || !donor.CallsToDonate.Any())
            {
                return Enumerable.Empty<CallToDonate>();
            }

            var calls = donor.CallsToDonate.Where(ctd => ctd.AcceptedTheCall == accepted && ctd.ShowedUp == showedUp);
            return calls;
        }

        public async Task<IEnumerable<CallToDonate?>> GetAllCalls(string JMBG)
        {
            var donor = await _context.Donors
              .Include(d => d.CallsToDonate)
              .Include(d => d.Place)
              .FirstOrDefaultAsync(d => d.JMBG == JMBG);

            if (donor == null || donor.CallsToDonate == null || !donor.CallsToDonate.Any())
            {
                return Enumerable.Empty<CallToDonate>();
            }

            return donor.CallsToDonate;
        }

        public async Task<IEnumerable<Donor?>> GetShowedUpDonors(int actionID)
        {
            var showedUpDonors = await _context.CallsToDonate
                .Where(ctd => ctd.ActionID == actionID && ctd.ShowedUp == true)
                .Include(ctd => ctd.Donor)
                    .ThenInclude(d => d.ListOfQuestionnaires)
                    .ThenInclude(q => q.ListOfQuestions)
                .Select(ctd => ctd.Donor)
                .ToListAsync();

            return showedUpDonors;
        }


    }
}
