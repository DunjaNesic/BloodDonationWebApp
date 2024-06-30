using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BloodDonationApp.DataAccessLayer.ActionRepo
{
    public class ActionRepository : RepositoryBase<TransfusionAction>, IActionRepository
    {
        private readonly BloodDonationContext _context;
        public ActionRepository(BloodDonationContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<TransfusionAction> GetAllActions(bool trackChanges)
        {
            var includes = new Expression<Func<TransfusionAction, object>>[]
            {
                 a => a.Place,
                 //hmm da li da stavim u TransfusionAction da ne moze da bude null
                 a => a.ListOfCallsToDonors,
                 a => a.ListOfCallsToVolunteers
            };

            var query = GetAll(trackChanges, includes);

            return query;
        }
        public async Task<TransfusionAction?> GetAction(int actionID)
        {
            var action = await _context.TransfusionActions.Where(a => a.ActionID == actionID).SingleOrDefaultAsync();

            return action;
        }

        public IQueryable<TransfusionAction> GetActionsByCondition(Expression<Func<TransfusionAction, bool>> condition, bool trackChanges)
        {
            var includes = new Expression<Func<TransfusionAction, object>>[]
            {
                a => a.Place
            };

            var query = GetByCondition(condition, trackChanges, includes);

            return query;
        }
    }
}
