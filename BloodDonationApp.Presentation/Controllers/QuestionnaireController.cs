using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.DataTransferObject.Questionnaires;
using BloodDonationApp.DataTransferObject.Volunteers;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.Presentation.ActionFilters;
using BloodDonationApp.DataTransferObject.Mappers;
using Common.RequestFeatures;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Presentation.Controllers
{
    [ApiController]
    [Route("itk/donors/{JMBG}/questionnaires")]
    public class QuestionnaireController : ApiBaseController
    {
        private readonly IServiceManager _serviceManager;
        public QuestionnaireController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetQuestionnaireDTO>>> GetAllQuestionnaires(string JMBG,
            [FromQuery] QuestionnaireParameters questionnaireParameters)
        {
            var baseResult = await _serviceManager.QuestionnaireService.GetAll(JMBG, questionnaireParameters, trackChanges: false);
            if (!baseResult.Success) return ProcessError(baseResult);
            var questionnaires = baseResult.GetResult<IEnumerable<GetQuestionnaireDTO>>();
            return Ok(questionnaires);
        }

        //[HttpPost]
        ////[ServiceFilter(typeof(ValidationFilterAttribute))]
        //public async Task<IActionResult> Create([FromBody] CreateQuestionnaireDTO questionnaire, string JMBG)
        //{
        //    try
        //    {
        //        var questions = await _serviceManager.QuestionService.GetAll(false);
        //        var createdQuestionnaire = QuestionnaireMapper.FromDto(questionnaire, questions);
        //        await _serviceManager.QuestionnaireService.Create(JMBG, createdQuestionnaire);
        //        return Ok(new { Message = "Questionnaire added successfully" });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { Message = ex.Message });
        //    }
        //}

    }
}
