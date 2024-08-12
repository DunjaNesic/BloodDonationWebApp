using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Contracts
{
    public interface IOfficialService
    {
        Task<ApiBaseResponse> GetAll(int officialsID);
        Task<ApiBaseResponse> GetOfficial(int userID);
    }
}
