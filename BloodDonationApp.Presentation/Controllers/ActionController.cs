using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.DataTransferObject.Volunteers;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BloodDonationApp.Presentation.Controllers
{
    [ApiController]
    [Route("itk/actions")]
    public class ActionController : ApiBaseController
    {
        private readonly IServiceManager _serviceManager;
        public ActionController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        //[HttpDelete("{actionID}")]
        //public async Task<IActionResult> Delete(int actionID)
        //{
        //    var baseResult = await _serviceManager.ActionService.Delete(actionID);
        //    if (!baseResult.Success) return ProcessError(baseResult);
        //    else return Ok(new { Message = "Action deleted successfully" });
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> GetAllActions()
        {
            var baseResult = await _serviceManager.ActionService.GetAll(false);
            if (!baseResult.Success) return ProcessError(baseResult);

            var actions = baseResult.GetResult<IEnumerable<GetTransfusionActionDTO>>();

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

        [HttpGet("jmbg/{JMBG}")]
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> GetAllActionsForDonor(string JMBG)
        {
            var baseRes = await _serviceManager.DonorService.GetByCondition(JMBG);
            if (!baseRes.Success) return ProcessError(baseRes);

            var donor = baseRes.GetResult<GetDonorDTO>();

            var baseResult = await _serviceManager.DonorService.GetDonorsActions(donor.JMBG);
            if (!baseResult.Success) return ProcessError(baseResult);

            var actions = baseResult.GetResult<IEnumerable<GetTransfusionActionDTO>>();

            return Ok(actions);
        }

        [HttpGet("volunteer/{volunteerID}")]
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> GetAllActionsForVolunteer(int volunteerID)
        {
            var baseRes = await _serviceManager.VolunteerService.GetVolunteer(volunteerID);
            if (!baseRes.Success) return ProcessError(baseRes);

            var volunteer = baseRes.GetResult<GetVolunteerDTO>();

            var baseResult = await _serviceManager.VolunteerService.GetVolunteersActions(volunteer.VolunteerID);
            if (!baseResult.Success) return ProcessError(baseResult);

            var actions = baseResult.GetResult<IEnumerable<GetTransfusionActionDTO>>();

            return Ok(actions);
        }

        [HttpGet("name/{partialName}")]
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> FindActionByName(string partialName)
        {
            Expression<Func<TransfusionAction, bool>> condition = a => a.ActionName.ToLower().Contains(partialName.ToLower());
            var baseResult = await _serviceManager.ActionService.GetByCondition(condition, false);

            if (!baseResult.Success)
                return ProcessError(baseResult);

            var filteredActions = baseResult.GetResult<IEnumerable<GetTransfusionActionDTO>>();

            return Ok(filteredActions);
        }

        [HttpGet("date/{date}")]
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> FindActionsByDate(DateTime date)
        {
            Expression<Func<TransfusionAction, bool>> condition = a => a.ActionDate == date;
            var baseResult = await _serviceManager.ActionService.GetByCondition(condition, false);

            if (!baseResult.Success)
                return ProcessError(baseResult);

            var filteredActions = baseResult.GetResult<IEnumerable<GetTransfusionActionDTO>>();

            return Ok(filteredActions);
        }

        [HttpGet("place/{placeID}")]
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> FindActionByPlace(int placeID)
        {
            Expression<Func<TransfusionAction, bool>> condition = a => a.PlaceID == placeID;
            var baseResult = await _serviceManager.ActionService.GetByCondition(condition, false);

            if (!baseResult.Success)
                return ProcessError(baseResult);

            var filteredActions = baseResult.GetResult<IEnumerable<GetTransfusionActionDTO>>();

            return Ok(filteredActions);
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

        [HttpGet("incoming/donor/{JMBG}")]
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> CurrDonorAction(string JMBG)
        {
            var baseRes = await _serviceManager.DonorService.GetByCondition(JMBG);
            if (!baseRes.Success) return ProcessError(baseRes);

            var donor = baseRes.GetResult<GetDonorDTO>();

            var baseResult = await _serviceManager.DonorService.GetIncomingDonorAction(donor.JMBG);
            if (!baseResult.Success) return ProcessError(baseResult);

            var actions = baseResult.GetResult<IEnumerable<GetTransfusionActionDTO>>();

            return Ok(actions);
        }

        [HttpGet("incoming/volunteer/{volunteerID}")]
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> CurrVolAction(int volunteerID)
        {
            var baseRes = await _serviceManager.VolunteerService.GetVolunteer(volunteerID);
            if (!baseRes.Success) return ProcessError(baseRes);

            var volunteer = baseRes.GetResult<GetVolunteerDTO>();

            var baseResult = await _serviceManager.VolunteerService.GetIncomingVolunteerAction(volunteer.VolunteerID);
            if (!baseResult.Success) return ProcessError(baseResult);

            var actions = baseResult.GetResult<IEnumerable<GetTransfusionActionDTO>>();

            return Ok(actions);
        }
    }
}
