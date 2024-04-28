using BloodDonationApp.Domain.ExceptionModel.Exceptions;
using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Volunteer;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.Presentation.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BloodDonationApp.Presentation
{
    [ApiController]
    [Route("api/volunteers")]
    public class VolunteerController : ApiBaseController
    {
        private readonly IServiceManager _serviceManager;
        public VolunteerController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<GetVolunteerDTO>>> GetAllVolunteers()
        //{
        //    var volunteers = await _serviceManager.VolunteerService.GetAll(trackChanges: false);
        //    return Ok(volunteers.Select(VolunteerMapper.ToDto).ToList());
        //}

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetVolunteerDTO>>> GetAllVolunteers()
        {
            var baseResult = await _serviceManager.VolunteerService.GetAll(trackChanges: false);
            var volunteers = ((ApiOkResponse<IQueryable<Volunteer>>)baseResult).Result;
            return Ok(volunteers.Select(VolunteerMapper.ToDto).ToList());
        }

        //[HttpGet]
        //[Route("{partialName}")]
        //public async Task<ActionResult<IEnumerable<GetVolunteerDTO>>> FindVolunteersByNameAsync(string partialName)
        //{
        //    Expression<Func<Volunteer, bool>> condition = v => v.VolunteerFullName.ToLower().Contains(partialName.ToLower());
        //    var filteredVolunteers = await _serviceManager.VolunteerService.GetByCondition(condition, false);
        //    if (filteredVolunteers == null || !filteredVolunteers.Any())
        //    {
        //        throw new Exception();
        //    }
        //    return Ok(filteredVolunteers.Select(VolunteerMapper.ToDto).ToList());
        //}

        [HttpGet]
        [Route("{partialName}")]
        public async Task<ActionResult<IQueryable<GetVolunteerDTO>>> FindVolunteersByNameAsync(string partialName)
        {
            Expression<Func<Volunteer, bool>> condition = v => v.VolunteerFullName.ToLower().Contains(partialName.ToLower());
            var baseResult = await _serviceManager.VolunteerService.GetByCondition(condition, false);
            if (!baseResult.Success) return ProcessError(baseResult);

            var filteredVolunteers = baseResult.GetResult<IQueryable<Volunteer>>();
            return Ok(filteredVolunteers.Select(VolunteerMapper.ToDto).ToList());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateVolunteerDTO volunteer)
        {
            try
            {
                await _serviceManager.VolunteerService.Create(volunteer.FromDto());
                return Ok(new { Message = "Volunteer added successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = ex.Message });
            }
        }

        [HttpDelete("{volunteerID}")]
        public async Task Delete(int volunteerID)
        {
            await _serviceManager.VolunteerService.Delete(new Volunteer()
            {
                VolunteerID = volunteerID
            });
        }

        [HttpPut("{volunteerID}")]
        public async Task Update(int volunteerID, [FromBody] UpdateVolunteerDTO volunteer)
        {
            await _serviceManager.VolunteerService.Update(volunteer.FromDto(), volunteerID);
        }

    }
}
