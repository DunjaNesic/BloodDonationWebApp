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
