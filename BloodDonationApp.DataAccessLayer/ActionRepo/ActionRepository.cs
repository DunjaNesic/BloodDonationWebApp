using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.ActionRepo
{
    public class ActionRepository : IActionRepository
    {
        private readonly BloodDonationContext _context;
        public ActionRepository(BloodDonationContext context)
        {
            _context = context;
        }
        public Task CreateAsync(TransfusionAction t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(TransfusionAction t)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TransfusionAction>> GetAllAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<TransfusionAction>> GetByConditionAsync(Expression<Func<TransfusionAction, bool>> condition, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TransfusionAction t)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TransfusionAction t, TransfusionAction tt)
        {
            throw new NotImplementedException();
        }
    }
}
