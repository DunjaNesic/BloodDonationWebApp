using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Donor;
using BloodDonationApp.DataTransferObject.Questionnaire;
using BloodDonationApp.DataTransferObject.Volunteer;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.Presentation.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/donors/{JMBG}/questionnaires")]
    public class QuestionnaireController : ApiBaseController
    {
        private readonly IServiceManager _serviceManager;
        public QuestionnaireController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        //samo cu getAll i create
        //public async Task<ActionResult<IEnumerable<GetQuestionnaireDTO>>> GetAllQuestionnairesAsync()
        //{
        //    var baseResult = await _serviceManager.QuestionService.GetAll(trackChanges: false);
        //    var questionnaires = ((ApiOkResponse<IQueryable<Questionnaire>>)baseResult).Result;
        //    return Ok(questionnaires.Select(QuestionnaireMapper.ToDto).ToList());
        //}

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] Questionnaire questionnaire)
        //{
        //    try
        //    {
        //        await _serviceManager.QuestionnaireService.Create(questionnaire);
        //        return Ok(new { Message = "Questionnaire added successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = ex.Message });
        //    }
        //}

    }
}
