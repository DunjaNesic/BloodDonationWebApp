using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using Common.RequestFeatures;
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
        Task<ApiBaseResponse> GetAll(bool trackChanges, DonorParameters donorParameters);
        Task<ApiBaseResponse> GetByCondition(string JMBG);
        Task<ApiBaseResponse> GetDonorsActions(string jMBG);
        Task<ApiBaseResponse> GetIncomingDonorAction(string jMBG);
        Task<ApiBaseResponse> CallDonor(string JMBG, int actionID);
        Task<ApiBaseResponse> UpdateCallToDonor(string JMBG, int actionID, CallsToDonorDTO donorCall);
    }
}
