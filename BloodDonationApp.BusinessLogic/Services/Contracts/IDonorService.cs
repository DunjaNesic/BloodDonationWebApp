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
    public interface IDonorService
    {
        Task<ApiBaseResponse> GetAll(bool trackChanges);
        Task<ApiBaseResponse> GetByCondition(Expression<Func<Donor, bool>> condition, bool trackChanges);
    }
}
