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
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
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
            var actions = await uow.VolunteerRepository.GetIncomingAction(volunteerID, false);

            if (!actions.Any())
            {
                return new ActionNotFoundResponse();
            }
            var actionsDTO = actions.Select(a => _actionMapper.ToDto(a)).ToList();
            return new ApiOkResponse<IEnumerable<GetTransfusionActionDTO>>(actionsDTO);
        }

        public async Task<ApiBaseResponse> CallVolunteer(int volunteerID, int actionID)
        {
            var volunteer = await uow.VolunteerRepository.GetVolunteer(vol => vol.VolunteerID == volunteerID, false);
            if (volunteer == null) return new VolunteerNotFoundResponse();
            
            var action = await uow.ActionRepository.GetAction(actionID);
            if (action == null) return new ActionNotFoundResponse();

            bool accepted = true;
            await uow.VolunteerCallsRepository.CreateCall(volunteerID, actionID, accepted);
            await uow.SaveChanges();
            return new ApiOkResponse<string>("woo");
        }

        public async Task<ApiBaseResponse> UpdateCallToVolunteer(int volunteerID, int actionID, CallsToVolunteerDTO volunteerCall)
        {
            var volunteer = await uow.VolunteerRepository.GetVolunteer(v => v.VolunteerID == volunteerID, false);
            if (volunteer == null) return new VolunteerNotFoundResponse();

            var action = await uow.ActionRepository.GetAction(actionID);
            if (action == null) return new ActionNotFoundResponse();

            var call = await uow.VolunteerCallsRepository.GetCall(volunteerID, actionID, true);
            if (call == null) return new VolunteerNotFoundResponse();

            if (call.AcceptedTheCall == true && call.ShowedUp == false && volunteerCall.AcceptedTheCall == true && volunteerCall.ShowedUp == false) {
                return new ApiOkResponse<string>("Vec ste prijavljeni na ovu akciju");
            }

            call.AcceptedTheCall = volunteerCall.AcceptedTheCall;
            call.ShowedUp = volunteerCall.ShowedUp;

            await uow.SaveChanges();
            return new ApiOkResponse<string>("Uspesno azuriran poziv na akciju");
        }

        public async Task<ApiBaseResponse> GetVolunteersNotifications(int volunteerID, bool history)
        {
            IEnumerable<TransfusionAction> actions;

            if (!history)
            {
                actions = await uow.VolunteerRepository.GetIncomingAction(volunteerID, true);
            }
            else
            {
                actions = await uow.VolunteerRepository.GetVolunteersHistory(volunteerID);
            }
            if (!actions.Any())
            {
                return new ActionNotFoundResponse();
            }
            var actionsDTO = actions.Select(a => _actionMapper.ToDto(a)).ToList();
            return new ApiOkResponse<IEnumerable<GetTransfusionActionDTO>>(actionsDTO);
        }

        public async Task<ApiBaseResponse> GetVolunteerStats(GetVolunteerDTO foundVolunteer)
        {
            var allCalls = await uow.VolunteerCallsRepository.GetAllCalls(foundVolunteer.VolunteerID);
            if (!allCalls.Any())
            {
                return new VolunteerNotFoundResponse();
            }

            var acceptedAndAttendedCalls = await uow.VolunteerCallsRepository.GetAACalls(foundVolunteer.VolunteerID);
            var acceptedButNotAttendedCalls = await uow.VolunteerCallsRepository.GetADCalls(foundVolunteer.VolunteerID);
            var declinedAndNotAttendedCalls = await uow.VolunteerCallsRepository.GetDDCalls(foundVolunteer.VolunteerID);
            var declinedButAttendedCalls = await uow.VolunteerCallsRepository.GetDACalls(foundVolunteer.VolunteerID);

            int totalActions = allCalls.Count();
            int acceptedAndAttended = acceptedAndAttendedCalls.Count();
            int acceptedButNotAttended = acceptedButNotAttendedCalls.Count();
            int declinedAndNotAttended = declinedAndNotAttendedCalls.Count();
            int declinedButAttended = declinedButAttendedCalls.Count();

            VolunteerStatisticsDTO volunteerStats = new VolunteerStatisticsDTO
            {
                VolunteerID = foundVolunteer.VolunteerID,
                FullName = foundVolunteer.VolunteerFullName,
                TotalActions = totalActions,
                AcceptedAndAttendedPercentage = (double)acceptedAndAttended / totalActions * 100,
                AcceptedButDidNotAttendPercentage = (double)acceptedButNotAttended / totalActions * 100,
                DeclinedAndDidNotAttendPercentage = (double)declinedAndNotAttended / totalActions * 100,
                DeclinedButAttendedPercentage = (double)declinedButAttended / totalActions * 100,
            };

            return new ApiOkResponse<VolunteerStatisticsDTO>(volunteerStats);
        }

        public async Task<ApiBaseResponse> GetCalledVolunteers(int actionID, bool v)
        {
            var calledVolunteers = await uow.VolunteerRepository.GetCalledVolunteersAsync(actionID, v);

            if (calledVolunteers == null || !calledVolunteers.Any())
            {
                return new DonorNotFoundResponse();
            }

            var calledVolunteersDTO = calledVolunteers.Select(v => _mapper.ToDto(v)).ToList();

            return new ApiOkResponse<IEnumerable<GetVolunteerDTO>>(calledVolunteersDTO);
        }

        public async Task<ApiBaseResponse> CallVolunteers(int[]? volunteerIDs, int actionID)
        {
            var res = await uow.VolunteerCallsRepository.CreateCalls(volunteerIDs, actionID);
            if (res == null) return new VolunteerNotFoundResponse();
            else return new ApiOkResponse<object>(res);
        }
    }
}
