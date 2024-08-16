using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using Common.RequestFeatures;
using Microsoft.AspNetCore.Mvc;


namespace BloodDonationApp.Presentation.Controllers
{
    [ApiController]
    [Route("itk/donors")]
    public class DonorController : ApiBaseController
    {
        private readonly IServiceManager _serviceManager;
        
        public DonorController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDonorDTO>>> GetAllDonors([FromQuery] DonorParameters donorParameters)
        {
            var baseResult = await _serviceManager.DonorService.GetAll(trackChanges: false, donorParameters);
            if (!baseResult.Success) return ProcessError(baseResult);
        
            var donors = baseResult.GetResult<IEnumerable<GetDonorDTO>>();

            return Ok(donors);
        }

        [HttpGet("{JMBG}")]
        public async Task<ActionResult<GetDonorDTO>> FindDonorByJMBG(string JMBG)
        {
            var baseResult = await _serviceManager.DonorService.GetByCondition(JMBG);
            if (!baseResult.Success) return ProcessError(baseResult);

            var foundDonor = baseResult.GetResult<GetDonorDTO>();

            return Ok(foundDonor);
        }

        [HttpPost]
        [Route("{JMBG}/{actionID}")]
        public async Task<IActionResult> ActionSignUp([FromRoute] string JMBG, int actionID)
        {
            var baseResult = await _serviceManager.DonorService.CallDonor(JMBG, actionID);
            if (!baseResult.Success) return ProcessError(baseResult);

            var call = baseResult.GetResult<string>();

            return Ok(call);
        }

        [HttpPost]
        public async Task<IActionResult> ActionCall([FromBody] CreateDonorCallsDTO calls)
        {
            var baseResult = await _serviceManager.DonorService.CallDonors(calls.JMBGs, calls.ActionID);
            if (!baseResult.Success) return ProcessError(baseResult);

            var call = baseResult.GetResult<object>();

            return Ok(call);
        }

        [HttpPut]
        [Route("{JMBG}/{actionID}")]
        public async Task<IActionResult> UpdateCall([FromRoute] string JMBG, int actionID, [FromBody] CallsToDonorDTO donorCall)
        {
            var baseResult = await _serviceManager.DonorService.UpdateCallToDonor(JMBG, actionID, donorCall);
            if (!baseResult.Success) return ProcessError(baseResult);

            var call = baseResult.GetResult<string>();

            return Ok(call);
        }

        [HttpGet("{JMBG}/calls/{history}")]
        public async Task<ActionResult<GetTransfusionActionDTO>> GetDonorsNotifications(string JMBG, bool history)
        {
            var baseResult = await _serviceManager.DonorService.GetByCondition(JMBG);
            if (!baseResult.Success) return ProcessError(baseResult);

            var notifsBaseRes = await _serviceManager.DonorService.GetDonorsNotifications(JMBG, history);
            if (!notifsBaseRes.Success) return ProcessError(notifsBaseRes);

            return Ok(notifsBaseRes.GetResult<IEnumerable<GetTransfusionActionDTO>>());
        }

        [HttpGet("present")]
        public async Task<ActionResult<GetDonorDTO>> GetPresentDonors(int actionID)
        {
            var baseResult = await _serviceManager.DonorService.GetAllPresentDonors(actionID);
            if (!baseResult.Success) return ProcessError(baseResult);

            return Ok(baseResult.GetResult<IEnumerable<GetDonorQuestionnaireDTO>>());
        }

        [HttpGet("{JMBG}/stats")]
        public async Task<ActionResult<DonorStatisticsDTO>> GetDonorStatistics(string JMBG)
        {
            var baseResult = await _serviceManager.DonorService.GetByCondition(JMBG);
            if (!baseResult.Success) return ProcessError(baseResult);

            var foundDonor = baseResult.GetResult<GetDonorDTO>();

            var baseResultStats = await _serviceManager.DonorService.GetDonorStats(foundDonor);
            if (!baseResultStats.Success) return ProcessError(baseResultStats);

            var stats = baseResultStats.GetResult<DonorStatisticsDTO>();

            return Ok(stats);
        }

    }
}