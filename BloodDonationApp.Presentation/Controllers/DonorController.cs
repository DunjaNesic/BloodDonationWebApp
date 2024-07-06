using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Donors;
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

    }
}