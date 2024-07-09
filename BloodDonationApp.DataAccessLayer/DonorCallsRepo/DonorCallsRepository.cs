using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<CallToDonate?> GetCall(string JMBG, int actionID, bool trackChanges)
        {
            var call = await GetByCondition((c => c.JMBG.Equals(JMBG) && c.ActionID == actionID), trackChanges).SingleOrDefaultAsync();
            return call;
        }
    }
}
