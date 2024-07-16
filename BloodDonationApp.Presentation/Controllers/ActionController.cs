using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.DataTransferObject.Volunteers;
using BloodDonationApp.Domain.CustomModel;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.LinkModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Presentation.ActionFilters;
using Common.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Routing;
using Microsoft.Net.Http.Headers;
using System.Dynamic;
using System.Linq.Expressions;

namespace BloodDonationApp.Presentation.Controllers
{
    public enum UserType
    {
        Donor,
        Volunteer
    }

    [ApiController]
    //[ResponseCache(CacheProfileName = "120SecondsDuration")] 
    [OutputCache(PolicyName = "120SecondsDuration")]
    [EnableRateLimiting("SpecificActionsPolicy")]
    [Route("itk/actions")]
    public class ActionController : ApiBaseController
    {
        private readonly IServiceManager _serviceManager;
        private LinkGenerator _linkGenerator;
        public ActionController(IServiceManager serviceManager, LinkGenerator linkGenerator)
        {
            _serviceManager = serviceManager;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        //[Authorize(Roles = "Donor")]
        [ServiceFilter(typeof(ValidateMediaTypeAttribute))]
        public async Task<IActionResult> GetAllActions([FromQuery] ActionParameters actionParameters )
        {
            var baseResult = await _serviceManager.ActionService.GetAll(false, actionParameters);
            if (!baseResult.Success) return ProcessError(baseResult);

            var actions = baseResult.GetResult<IEnumerable<ShapedCustomExpando>>();
            List<ShapedCustomExpando> customActions = actions.ToList();
            List<CustomExpando> shapedActions = actions.Select(a => a.CustomExpando).ToList();

            var mediaType = HttpContext.Items["AcceptHeaderMediaType"] as MediaTypeHeaderValue
                            ?? throw new InvalidOperationException("AcceptHeaderMediaType is not set.");

            if (!mediaType.SubTypeWithoutSuffix.EndsWith("hateoas", StringComparison.InvariantCultureIgnoreCase))
            {
                return Ok(shapedActions);
            }

            for (int i = 0; i < shapedActions.Count(); i++)
            {
                var actionLinks = CreateLinksForAction(customActions[i].Id, actionParameters.Fields);
                shapedActions[i].Add("Links", actionLinks);
            }

            var actionsWrapper = new LinkWrapper<CustomExpando>(shapedActions);

            return Ok(CreateLinksForAction(actionsWrapper));
        }

        [HttpGet("{actionID}")]
        //[ResponseCache(Duration = 13)] 
        [OutputCache(Duration = 13)]
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
        public async Task<ActionResult<IEnumerable<GetTransfusionActionDTO>>> GetAllUsersActions(UserType userType, string id)
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

        private IEnumerable<Link> CreateLinksForAction(int id, string fields = "")
        {
            var links = new List<Link>
    {
        new Link(_linkGenerator.GetUriByAction(HttpContext, action: nameof(GetAction), controller: "Action",  values: new { actionID = id, fields }),
        "get_action",
        "GET"),

        //new Link(_linkGenerator.GetUriByAction(HttpContext, action: nameof(FindActionByOfficial), controller: "Action",  values: new {  officialsID = id }),
        //"get_officials_actions",
        //"GET"),

        //new Link(_linkGenerator.GetUriByAction(HttpContext, action: nameof(GetAllUsersActions), controller: "Action", values: new { userType = UserType.Volunteer, id }),
        //"get_users_actions",
        //"GET"),

        //new Link(_linkGenerator.GetUriByAction(HttpContext, action: nameof(GetCurrentActions), controller: "Action", values: new { userType = UserType.Volunteer, id }),
        //"get_current_actions",
        //"GET")
    };

            return links;
        }

        private LinkWrapper<CustomExpando> CreateLinksForAction(LinkWrapper<CustomExpando> actionsWrapper)
        {
            actionsWrapper.Links.Add(new Link(_linkGenerator.GetUriByAction(HttpContext, action: nameof(GetAllActions), controller: "Action", values: new { }),
                    "self",
                    "GET"));

            return actionsWrapper;
        }



    }
}
