using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Contracts
{
    public interface IActionService
    {
        Task<ApiBaseResponse> Delete(int actionID);
        Task<ApiBaseResponse> GetAll(bool trackChanges);
        Task<ApiBaseResponse> GetAction(int actionID);
        Task<ApiBaseResponse> GetByCondition(Expression<Func<TransfusionAction, bool>> condition, bool trackChanges);

    }
}
