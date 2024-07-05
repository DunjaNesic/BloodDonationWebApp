using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using Common.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.ActionRepo
{
    public interface IActionRepository : IRepository<TransfusionAction>
    {
        IQueryable<TransfusionAction> GetAllActions(bool trackChanges, ActionParameters actionParameters);
        Task<TransfusionAction?> GetAction(int actionID);
        IQueryable<TransfusionAction> GetActionsByCondition(Expression<Func<TransfusionAction, bool>> condition, bool trackChanges);

    }
}
