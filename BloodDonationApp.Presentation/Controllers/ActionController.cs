using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.DataTransferObject.Volunteers;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using Common.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BloodDonationApp.Presentation.Controllers
{
    public enum UserType
    {
        Donor,
        Volunteer
    }

    [ApiController]
    [Route("itk/actions")]
    public class ActionController : ApiBaseController
    {
        private readonly IServiceManager _serviceManager;
        public ActionController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> GetAllActions([FromQuery] ActionParameters actionParameters )
        {
            var baseResult = await _serviceManager.ActionService.GetAll(false, actionParameters);
            if (!baseResult.Success) return ProcessError(baseResult);

            var actions = baseResult.GetResult<IEnumerable<ExpandoObject>>();

            return Ok(actions);
        }

        [HttpGet("{actionID}")]
        public async Task<ActionResult<GetTransfusionActionDTO>> GetAction(int actionID)
        {
            var baseResult = await _serviceManager.ActionService.GetAction(actionID);
            if (!baseResult.Success) return ProcessError(baseResult);

            var actions = baseResult.GetResult<GetTransfusionActionDTO>();

            return Ok(actions);
        }

        [HttpGet("official/{officialsID}")]
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> FindActionByOfficial(int officialsID)
        {
            Expression<Func<TransfusionAction, bool>> condition = a => a.OfficialID == officialsID;
            var baseResult = await _serviceManager.ActionService.GetByCondition(condition, false);

            if (!baseResult.Success)
                return ProcessError(baseResult);

            var filteredActions = baseResult.GetResult<IEnumerable<GetTransfusionActionDTO>>();

            return Ok(filteredActions);
        }

        [HttpGet("{userType}/{id}")]
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> GetAllActions(UserType userType, string id)
        {
            switch (userType)
            {
                case UserType.Donor:
                    var donorRes = await _serviceManager.DonorService.GetByCondition(id);
                    if (!donorRes.Success) return ProcessError(donorRes);

                    var donor = donorRes.GetResult<GetDonorDTO>();
                    var donorActionsRes = await _serviceManager.DonorService.GetDonorsActions(donor.JMBG);
                    if (!donorActionsRes.Success) return ProcessError(donorActionsRes);

                    var donorActions = donorActionsRes.GetResult<IEnumerable<GetTransfusionActionDTO>>();
                    return Ok(donorActions);

                case UserType.Volunteer:
                    if (!int.TryParse(id, out int volunteerID))
                    {
                        return BadRequest("Invalid volunteer ID format.");
                    }

                    var volunteerRes = await _serviceManager.VolunteerService.GetVolunteer(volunteerID);
                    if (!volunteerRes.Success) return ProcessError(volunteerRes);

                    var volunteer = volunteerRes.GetResult<GetVolunteerDTO>();
                    var volunteerActionsRes = await _serviceManager.VolunteerService.GetVolunteersActions(volunteer.VolunteerID);
                    if (!volunteerActionsRes.Success) return ProcessError(volunteerActionsRes);

                    var volunteerActions = volunteerActionsRes.GetResult<IEnumerable<GetTransfusionActionDTO>>();
                    return Ok(volunteerActions);

                default:
                    return BadRequest("Invalid user type.");
            }
        }


        [HttpGet("incoming/{userType}/{id}")]
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> GetCurrentActions(UserType userType, string id)
        {
            switch (userType)
            {
                case UserType.Donor:
                    var donorRes = await _serviceManager.DonorService.GetByCondition(id);
                    if (!donorRes.Success) return ProcessError(donorRes);

                    var donor = donorRes.GetResult<GetDonorDTO>();
                    var donorActionsRes = await _serviceManager.DonorService.GetIncomingDonorAction(donor.JMBG);
                    if (!donorActionsRes.Success) return ProcessError(donorActionsRes);

                    var donorActions = donorActionsRes.GetResult<IEnumerable<GetTransfusionActionDTO>>();
                    return Ok(donorActions);

                case UserType.Volunteer:
                    if (!int.TryParse(id, out int volunteerID))
                    {
                        return BadRequest("Invalid volunteerID format");
                    }

                    var volunteerRes = await _serviceManager.VolunteerService.GetVolunteer(volunteerID);
                    if (!volunteerRes.Success) return ProcessError(volunteerRes);

                    var volunteer = volunteerRes.GetResult<GetVolunteerDTO>();
                    var volunteerActionsRes = await _serviceManager.VolunteerService.GetIncomingVolunteerAction(volunteer.VolunteerID);
                    if (!volunteerActionsRes.Success) return ProcessError(volunteerActionsRes);

                    var volunteerActions = volunteerActionsRes.GetResult<IEnumerable<GetTransfusionActionDTO>>();
                    return Ok(volunteerActions);

                default:
                    return BadRequest("Invalid userType");
            }
        }     
    }
}
