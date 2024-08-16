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
         
            if (!baseResult.Success)
                return ProcessError(baseResult);

            var questionnaires = baseResult.GetResult<IEnumerable<GetQuestionnaireDTO>>();

            return Ok(questionnaires);
        }

        [HttpGet("questions")]
        public async Task<ActionResult<IEnumerable<Question>>> GetAllQuestions(string JMBG)
        {
            var baseResult = await _serviceManager.QuestionService.GetQuestionsForDonor(false);

            if (!baseResult.Success)
                return ProcessError(baseResult);

            var questions = baseResult.GetResult<IEnumerable<Question>>();

            return Ok(questions);
        }

        [HttpGet("/itk/questions")]
        public async Task<ActionResult<IEnumerable<Question>>> GetAlll()
        {
            var baseResult = await _serviceManager.QuestionService.GetAllQuestions(false);

            if (!baseResult.Success)
                return ProcessError(baseResult);

            var questions = baseResult.GetResult<IEnumerable<Question>>();

            return Ok(questions);
        }

        [HttpGet("{actionID}")]
        public async Task<ActionResult<GetQuestionnaireDTO>> GetQuestionnaire(string JMBG, int actionID)
        {
            var baseResult = await _serviceManager.QuestionnaireService.Get(JMBG, actionID);

            if (!baseResult.Success)
                return ProcessError(baseResult);

            var questionnaires = baseResult.GetResult<GetQuestionnaireDTO>();

            return Ok(questionnaires);
        }

        [HttpPost("{actionID}")]
        //[ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> Create(string JMBG, int actionID, [FromBody] CreateQuestionnaireDTO createdQuestionnaire)
        {
             var baseResult = await _serviceManager.QuestionnaireService.Create(JMBG, actionID, createdQuestionnaire);
             
             if (!baseResult.Success)
                return ProcessError(baseResult);

             var questionnaires = baseResult.GetResult<GetQuestionnaireDTO>();

             return Ok(questionnaires);            
        }

        [HttpPut("{actionID}")]
        public async Task<ActionResult<GetQuestionnaireDTO>> Update(string JMBG, int actionID, [FromBody] UpdateQuestionnaireDTO updatedQuestionnaire)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine($"Validation error: {error.ErrorMessage}");
                }
                return BadRequest(ModelState); 
            }
            var baseResult = await _serviceManager.QuestionnaireService.Update(JMBG, actionID, updatedQuestionnaire);

            if (!baseResult.Success)
                return ProcessError(baseResult);

            var updatedQuestionnaireDto = baseResult.GetResult<GetQuestionnaireDTO>();

            return Ok(updatedQuestionnaireDto);
        }
    }
}
