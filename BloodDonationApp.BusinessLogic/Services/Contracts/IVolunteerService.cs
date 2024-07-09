using BloodDonationApp.DataTransferObject.Volunteers;
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
    public interface IVolunteerService
    {
        Task<ApiBaseResponse> GetAll(bool trackChanges, VolunteerParameters volunteerParameters);
        Task<ApiBaseResponse> GetByCondition(Expression<Func<Volunteer, bool>> condition, bool trackChanges);
        Task<ApiBaseResponse> GetVolunteer(int volunteerID);
        Task<ApiBaseResponse> GetVolunteersActions(int volunteerID);
        Task<ApiBaseResponse> GetIncomingVolunteerAction(int volunteerID);
        Task<ApiBaseResponse> CallVolunteer(int volunteerID, int actionID);
        Task<ApiBaseResponse> UpdateCallToVolunteer(int volunteerID, int actionID, CallsToVolunteerDTO volunteerCall);
    }
}
