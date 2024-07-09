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
    }
}
