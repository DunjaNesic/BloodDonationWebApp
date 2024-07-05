using BloodDonationApp.BusinessLogic.ServerSideValidation;
using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.DataTransferObject.Mappers;
using BloodDonationApp.DataTransferObject.Volunteers;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Action;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Donor;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Volunteer;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.LoggerService;
using Common.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class VolunteerService : IVolunteerService
    {
        private readonly IUnitOfWork uow;
        private readonly ILoggerManager _logger;
        private readonly VolunteerMapper _mapper = new VolunteerMapper();
        private readonly ActionMapper _actionMapper = new ActionMapper();

        public VolunteerService(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            uow = unitOfWork;
            _logger = logger;

        }
        //    public async Task<ApiBaseResponse> GetAll(bool trackChanges)
        //{        
        //    //var volunteers = await uow.VolunteerRepository.GetAllAsync(trackChanges);
        //    //return new ApiOkResponse<IQueryable<Volunteer>>(volunteers);
        //}

        //public async Task<ApiBaseResponse> GetByCondition(Expression<Func<Volunteer, bool>> condition, bool trackChanges)
        //{
        //    //var filteredVolunteers = await uow.VolunteerRepository.GetByConditionAsync(condition, trackChanges);
        //    //if (filteredVolunteers.IsNullOrEmpty()) return new VolunteerNotFoundResponse();
        //    //return new ApiOkResponse<IQueryable<Volunteer>>(filteredVolunteers);
        //}

        public async Task<ApiBaseResponse> Create(Volunteer v)
        {
            //VolunteerValidation validator = new();
            //var errorMessages = validator.Validate(v);

            //if (errorMessages.Count != 0)
            //{
            //    foreach (var message in errorMessages)
            //    {
            //        Console.WriteLine(message);
            //    }
            //    return new VolunteerUnprocessableEntityResponse();
            //}
            await uow.VolunteerRepository.CreateAsync(v);
            await uow.SaveChanges();
            return new ApiOkResponse<Volunteer>(v);
        }
        public async Task<ApiBaseResponse> Delete(int volunteerID)
        {
            Expression<Func<Volunteer, bool>> condition = vol => vol.VolunteerID == volunteerID;
            var volunteerToDelete = await uow.VolunteerRepository.GetVolunteer(condition, true);
            if (volunteerToDelete == null) return new VolunteerNotFoundResponse();
            uow.VolunteerRepository.Delete(volunteerToDelete);
            await uow.SaveChanges();
            return new ApiOkResponse<string>("Volunteer deleted successfully");
        }

        public async Task<ApiBaseResponse> GetAll(bool trackChanges, VolunteerParameters volunteerParameters)
        {
            _logger.LogInformation("GetAll from VolunteerService");
            var query = uow.VolunteerRepository.GetAllVolunteers(trackChanges, volunteerParameters);
            var volunteers = await query.ToListAsync();
            var volunteersDTO = volunteers.Select(volunteer => _mapper.ToDto(volunteer)).ToList();
            return new ApiOkResponse<IEnumerable<GetVolunteerDTO>>(volunteersDTO);
        }

        public async Task<ApiBaseResponse> GetVolunteer(int volunteerID)
        {
            _logger.LogInformation("GetVolunteer from VolunteerService");
            Expression<Func<Volunteer, bool>> condition = vol => vol.VolunteerID == volunteerID;
            var volunteer = await uow.VolunteerRepository.GetVolunteer(condition, false);
            if (volunteer == null) return new VolunteerNotFoundResponse();
            var volunteersDTO = _mapper.ToDto(volunteer);
            return new ApiOkResponse<GetVolunteerDTO>(volunteersDTO);
        }

        public async Task<ApiBaseResponse> GetByCondition(Expression<Func<Volunteer, bool>> condition, bool trackChanges)
        {
            var query = uow.VolunteerRepository.GetVolunteersByCondition(condition, trackChanges);
            var foundVolunteers = await query.ToListAsync();
            if (foundVolunteers.IsNullOrEmpty()) return new VolunteerNotFoundResponse();
            var foundVolunteersDTO = foundVolunteers.Select(volunteer => _mapper.ToDto(volunteer)).ToList();
            if (foundVolunteersDTO.IsNullOrEmpty()) return new VolunteerNotFoundResponse();
            return new ApiOkResponse<IEnumerable<GetVolunteerDTO>>(foundVolunteersDTO);
        }

        public async Task Update(Volunteer v, int volunteerID)
        {
            Expression<Func<Volunteer, bool>> condition = vol => vol.VolunteerID == volunteerID;
            var existingVolunteer = await uow.VolunteerRepository.GetVolunteer(condition, true);

            if (existingVolunteer == null)
                return;

            //if (existingVolunteer.RowVersion.SequenceEqual(v.RowVersion) == false)
            //{
            //    throw new Exception("Radite sa zastarelim podacima o volonteru");
            //}

            //existingVolunteer.RowVersion = v.RowVersion;
            existingVolunteer.VolunteerFullName = v.VolunteerFullName;
            existingVolunteer.VolunteerEmailAddress = v.VolunteerEmailAddress;
            existingVolunteer.RedCrossID = v.RedCrossID;
            existingVolunteer.DateFreeFrom = v.DateFreeFrom;
            existingVolunteer.DateFreeTo = v.DateFreeTo;
            existingVolunteer.DateOfBirth = v.DateOfBirth;

            //await uow.VolunteerRepository.UpdateAsync(v);
            await uow.SaveChanges();
        }

        public async Task<ApiBaseResponse> GetVolunteersActions(int volunteerID)
        {
            var actions = await uow.VolunteerRepository.GetActions(volunteerID);

            if (!actions.Any())
            {
                return new ActionNotFoundResponse();
            }
            var actionsDTO = actions.Select(a => _actionMapper.ToDto(a)).ToList();
            return new ApiOkResponse<IEnumerable<GetTransfusionActionDTO>>(actionsDTO);
        }

        public async Task<ApiBaseResponse> GetIncomingVolunteerAction(int volunteerID)
        {
            var actions = await uow.VolunteerRepository.GetIncomingAction(volunteerID);

            if (!actions.Any())
            {
                return new ActionNotFoundResponse();
            }
            var actionsDTO = actions.Select(a => _actionMapper.ToDto(a)).ToList();
            return new ApiOkResponse<IEnumerable<GetTransfusionActionDTO>>(actionsDTO);
        }
    }
}
