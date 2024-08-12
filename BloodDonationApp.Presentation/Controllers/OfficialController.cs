using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.DataTransferObject.Officials;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Presentation.Controllers
{
    [ApiController]
    [Route("itk/officials")]
    public class OfficialController : ApiBaseController
    {
        private readonly IServiceManager _serviceManager;
        public OfficialController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }


        [HttpGet("{userID}")]
        public async Task<ActionResult<GetOfficialDTO>> FindOfficialByUserID(int userID)
        {
            var baseResult = await _serviceManager.OfficialService.GetOfficial(userID);
            if (!baseResult.Success) return ProcessError(baseResult);

            var foundOfficial = baseResult.GetResult<GetOfficialDTO>();

            return Ok(foundOfficial);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetOfficialDTO>>> GetAll(int officialsID)
        {
            var baseResult = await _serviceManager.OfficialService.GetAll(officialsID);
            if (!baseResult.Success) return ProcessError(baseResult);

            var officials = baseResult.GetResult<IEnumerable<GetOfficialDTO>>();

            return Ok(officials);
        }


    }
}
