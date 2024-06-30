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
    public interface IVolunteerService
    {
        Task<ApiBaseResponse> GetAll(bool trackChanges);
        Task<ApiBaseResponse> GetByCondition(Expression<Func<Volunteer, bool>> condition, bool trackChanges);
        Task<ApiBaseResponse> GetVolunteer(int volunteerID);
        Task<ApiBaseResponse> Create(Volunteer v);
        Task Update(Volunteer v, int volunteerID);
        Task<ApiBaseResponse> Delete(int volunteerID);
        Task<ApiBaseResponse> GetVolunteersActions(int volunteerID);
        Task<ApiBaseResponse> GetIncomingVolunteerAction(int volunteerID);
    }
}
