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
        public async Task<ActionResult<IEnumerable<GetVolunteerDTO>>> GetAllVolunteers()
        {
            var baseResult = await _serviceManager.VolunteerService.GetAll(trackChanges: false);
            if (!baseResult.Success) return ProcessError(baseResult);           

            var volunteers = baseResult.GetResult<IEnumerable<GetVolunteerDTO>>();
            return Ok(volunteers);
        }

        [HttpGet]
        [Route("{partialName}")]
        public async Task<ActionResult<IEnumerable<GetVolunteerDTO>>> FindVolunteersByName(string partialName)
        {
            Expression<Func<Volunteer, bool>> condition = v => v.VolunteerFullName.ToLower().Contains(partialName.ToLower());
            var baseResult = await _serviceManager.VolunteerService.GetByCondition(condition, false);

            if (!baseResult.Success)
                return ProcessError(baseResult);

            var filteredVolunteers = baseResult.GetResult<IEnumerable<GetVolunteerDTO>>();

            return Ok(filteredVolunteers);
        }


        //[HttpPost]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        //public async Task<IActionResult> Create([FromBody] CreateVolunteerDTO volunteer)
        //{         
        //    var baseResult = await _serviceManager.VolunteerService.Create(volunteer.FromDto());
        //    if (!baseResult.Success) return ProcessError(baseResult);
        //    else return Ok(new { Message = "Volunteer added successfully" });          
        //}

        //[HttpDelete("{volunteerID}")]
        //public async Task<IActionResult> Delete(int volunteerID)
        //{
        //   var baseResult = await _serviceManager.VolunteerService.Delete(volunteerID);
        //    if (!baseResult.Success) return ProcessError(baseResult);
        //    else return Ok(new { Message = "Volunteer deleted successfully" });
        //}

        //[HttpPut("{volunteerID}")]
        ////[ServiceFilter(typeof(ValidationFilterAttribute))]
        //public async Task Update(int volunteerID, [FromBody] UpdateVolunteerDTO volunteer)
        //{
        //    await _serviceManager.VolunteerService.Update(volunteer.FromDto(), volunteerID);
        //}

    }
}
