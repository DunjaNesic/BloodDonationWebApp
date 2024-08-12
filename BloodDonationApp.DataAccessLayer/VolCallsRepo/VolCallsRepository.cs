using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.VolCallsRepo
{
    public class VolCallsRepository : RepositoryBase<CallToVolunteer>, IVolCallsRepository
    {
        private readonly BloodDonationContext _context;
        public VolCallsRepository(BloodDonationContext context) : base(context)
        {
            _context = context;
        }
        public async Task CreateCall(int volunteerID, int actionID, bool accepted)
        {
            CallToVolunteer call = new CallToVolunteer()
            {
                VolunteerID = volunteerID,
                ActionID = actionID,
                AcceptedTheCall = accepted
            };
            await CreateAsync(call);
        }

        public async Task<CallToVolunteer?> GetCall(int volunteerID, int actionID, bool trackChanges)
        {
            var call = await GetByCondition((c => c.VolunteerID == volunteerID && c.ActionID == actionID), trackChanges).SingleOrDefaultAsync();
            return call;
        }

        public async Task<IEnumerable<CallToVolunteer?>> GetAllCalls(int volunteerID)
        {
            var vol = await _context.Volunteers
              .Include(d => d.CallsToVolunteer)
              .Include(d => d.RedCross)
              .FirstOrDefaultAsync(d => d.VolunteerID == volunteerID);

            if (vol == null || vol.CallsToVolunteer == null || !vol.CallsToVolunteer.Any())
            {
                return Enumerable.Empty<CallToVolunteer>();
            }

            return vol.CallsToVolunteer;
        }

        public async Task<IEnumerable<CallToVolunteer?>> GetAACalls(int volunteerID)
        {
            return await GetCallsByCriteria(volunteerID, accepted: true, showedUp: true);
        }

        public async Task<IEnumerable<CallToVolunteer?>> GetADCalls(int volunteerID)
        {
            return await GetCallsByCriteria(volunteerID, accepted: true, showedUp: false);
        }

        public async Task<IEnumerable<CallToVolunteer?>> GetDDCalls(int volunteerID)
        {
            return await GetCallsByCriteria(volunteerID, accepted: false, showedUp: false);
        }

        public async Task<IEnumerable<CallToVolunteer?>> GetDACalls(int volunteerID)
        {
            return await GetCallsByCriteria(volunteerID, accepted: false, showedUp: true);
        }

        private async Task<IEnumerable<CallToVolunteer?>> GetCallsByCriteria(int volunteerID, bool accepted, bool showedUp)
        {
            var volunteer = await _context.Volunteers
               .Include(d => d.CallsToVolunteer)
               .Include(d => d.RedCross)
               .FirstOrDefaultAsync(d => d.VolunteerID == volunteerID);

            if (volunteer == null || volunteer.CallsToVolunteer == null || !volunteer.CallsToVolunteer.Any())
            {
                return Enumerable.Empty<CallToVolunteer>();
            }

            var calls = volunteer.CallsToVolunteer.Where(ctv => ctv.AcceptedTheCall == accepted && ctv.ShowedUp == showedUp);
            return calls;
        }
    }
}
