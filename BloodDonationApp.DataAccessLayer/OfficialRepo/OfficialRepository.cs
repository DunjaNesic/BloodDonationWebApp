using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.OfficialRepo
{
    public class OfficialRepository : RepositoryBase<Official>, IOfficialRepository
    {
        private readonly BloodDonationContext _context;
        public OfficialRepository(BloodDonationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Official?> GetByUserID(int userID)
        {
            var official = await _context.Officials
                .Include(o => o.CreatedActions)
                .Include(o => o.ListOfActions)
                .Where(o => o.UserID == userID)
                .SingleOrDefaultAsync();
            return official;
        }

        public async Task<Official?> GetOfficial(int officialID)
        {
            var official = await _context.Officials
                .Include(o => o.CreatedActions)
                .Include(o => o.ListOfActions)
                .Where(o => o.OfficialID == officialID)
                .SingleOrDefaultAsync();
            return official;
        }
    }
}
