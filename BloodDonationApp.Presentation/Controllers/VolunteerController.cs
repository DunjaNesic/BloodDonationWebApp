using BloodDonationApp.Domain.ExceptionModel.Exceptions;
using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Volunteers;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;
using BloodDonationApp.Presentation.ActionFilters;
using Common.RequestFeatures;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Volunteer;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.DataTransferObject.Action;

namespace BloodDonationApp.Presentation
{
    [ApiController]
    [Route("itk/volunteers")]
    public class VolunteerController : ApiBaseController
    {
        private readonly IServiceManager _serviceManager;
        public VolunteerController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetVolunteerDTO>>> GetAllVolunteers([FromQuery] VolunteerParameters volunteerParameters)
        { 
            var baseResult = await _serviceManager.VolunteerService.GetAll(trackChanges: false, volunteerParameters);
            if (!baseResult.Success) return ProcessError(baseResult);           

            var volunteers = baseResult.GetResult<IEnumerable<GetVolunteerDTO>>();
            return Ok(volunteers);
        }

        [HttpGet]
        [Route("{volunteerID}")]
        public async Task<ActionResult<GetVolunteerDTO>> GetVolunteer(int volunteerID)
        {
            var baseResult = await _serviceManager.VolunteerService.GetVolunteer(volunteerID);
            if (!baseResult.Success) return ProcessError(baseResult);

            var foundVolunteer = baseResult.GetResult<GetVolunteerDTO>();

            return Ok(foundVolunteer);
        }

        [HttpPost]
        [Route("{volunteerID}/{actionID}")]
        public async Task<IActionResult> ActionSignUp([FromRoute]int volunteerID, int actionID)
        {
            var baseResult = await _serviceManager.VolunteerService.CallVolunteer(volunteerID, actionID);
            if (!baseResult.Success) return ProcessError(baseResult);

            var call = baseResult.GetResult<string>();

            return Ok(call);
        }

        [HttpPut]
        [Route("{volunteerID}/{actionID}")]
        public async Task<IActionResult> UpdateCall([FromRoute] int volunteerID, int actionID, [FromBody] CallsToVolunteerDTO volunteerCall)
        {
            var baseResult = await _serviceManager.VolunteerService.UpdateCallToVolunteer(volunteerID, actionID, volunteerCall);
            if (!baseResult.Success) return ProcessError(baseResult);

            var call = baseResult.GetResult<string>();

            return Ok(call);
        }

        [HttpGet("{volunteerID}/calls/{history}")]
        public async Task<ActionResult<GetTransfusionActionDTO>> GetVolunteersNotifications(int volunteerID, bool history)
        {
            var baseResult = await _serviceManager.VolunteerService.GetVolunteer(volunteerID);
            if (!baseResult.Success) return ProcessError(baseResult);

            var notifsBaseRes = await _serviceManager.VolunteerService.GetVolunteersNotifications(volunteerID, history);
            if (!notifsBaseRes.Success) return ProcessError(notifsBaseRes);

            return Ok(notifsBaseRes.GetResult<IEnumerable<GetTransfusionActionDTO>>());
        }

        [HttpGet("{volunteerID}/stats")]
        public async Task<ActionResult<DonorStatisticsDTO>> GetVolunteerStatistics(int volunteerID)
        {
            var baseResult = await _serviceManager.VolunteerService.GetVolunteer(volunteerID);
            if (!baseResult.Success) return ProcessError(baseResult);

            var foundVolunteer = baseResult.GetResult<GetVolunteerDTO>();

            var baseResultStats = await _serviceManager.VolunteerService.GetVolunteerStats(foundVolunteer);
            if (!baseResultStats.Success) return ProcessError(baseResultStats);

            var stats = baseResultStats.GetResult<VolunteerStatisticsDTO>();

            return Ok(stats);
        }

        [HttpPost]
        public async Task<IActionResult> ActionCall([FromBody] CreateVolunteerCallsDTO calls)
        {
            var baseResult = await _serviceManager.VolunteerService.CallVolunteers(calls.VolunteerIDs, calls.ActionID);
            if (!baseResult.Success) return ProcessError(baseResult);

            var call = baseResult.GetResult<object>();

            return Ok(call);
        }

    }
}
