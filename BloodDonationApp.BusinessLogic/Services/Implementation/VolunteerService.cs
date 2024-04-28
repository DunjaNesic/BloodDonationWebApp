using BloodDonationApp.BusinessLogic.ServerSideValidation;
using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Volunteer;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IUnitOfWork uow;
        public VolunteerService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }      
        public async Task<ApiBaseResponse> GetAll(bool trackChanges)
        {        
            var volunteers = await uow.VolunteerRepository.GetAllAsync(trackChanges);
            return new ApiOkResponse<IQueryable<Volunteer>>(volunteers);
        }

        public async Task<ApiBaseResponse> GetByCondition(Expression<Func<Volunteer, bool>> condition, bool trackChanges)
        {
            var allVolunteers = await uow.VolunteerRepository.GetAllAsync(trackChanges);
            var filteredVolunteers = allVolunteers.Where(condition);
            if (filteredVolunteers.IsNullOrEmpty()) return new VolunteerNotFoundResponse();
            return new ApiOkResponse<IQueryable<Volunteer>>(filteredVolunteers);
        }

        public async Task Create(Volunteer v)
        {
            VolunteerValidation validator = new();
            var errorMessages = validator.Validate(v);

            if (errorMessages.Count != 0)
            {
                foreach (var message in errorMessages)
                {
                    Console.WriteLine(message);
                }
                return;
            }
            await uow.VolunteerRepository.CreateAsync(v);
            await uow.SaveChanges();
        }
        public async Task Delete(Volunteer v)
        {
            Expression<Func<Volunteer, bool>> condition = vol => vol.VolunteerID == v.VolunteerID;
            var volunteerToDelete = await uow.VolunteerRepository.GetVolunteer(condition, true);
            if (volunteerToDelete == null) return;
            await uow.VolunteerRepository.DeleteAsync(volunteerToDelete);
            await uow.SaveChanges();
        }

        public async Task Update(Volunteer v, int volunteerID)
        {
            Expression<Func<Volunteer, bool>> condition = vol => vol.VolunteerID == volunteerID;
            var existingVolunteer = await uow.VolunteerRepository.GetVolunteer(condition, true);

            if (existingVolunteer == null)
                return;

            if (existingVolunteer.RowVersion.SequenceEqual(v.RowVersion) == false)
            {
                throw new Exception("Radite sa zastarelim podacima o volonteru");
            }

            existingVolunteer.RowVersion = v.RowVersion;
            existingVolunteer.VolunteerFullName = v.VolunteerFullName;
            existingVolunteer.VolunteerEmailAddress = v.VolunteerEmailAddress;
            existingVolunteer.PlaceID = v.PlaceID;
            existingVolunteer.DateFreeFrom = v.DateFreeFrom;
            existingVolunteer.DateFreeTo = v.DateFreeTo;
            existingVolunteer.DateOfBirth = v.DateOfBirth;

            //await uow.VolunteerRepository.UpdateAsync(v);
            await uow.SaveChanges();
        }
    }
}
