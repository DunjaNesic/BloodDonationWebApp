using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Questionnaires;
using BloodDonationApp.Domain.DomainModel;
using Common.RequestFeatures;
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
    [Route("itk/places")]
    public class PlaceController : ApiBaseController
    {
        private readonly IServiceManager _serviceManager;
        public PlaceController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Place>>> GetAllPlaces()
        {
            var baseResult = await _serviceManager.PlaceService.GetAll(trackChanges: false);

            if (!baseResult.Success)
                return ProcessError(baseResult);

            var places = baseResult.GetResult<IEnumerable<Place>>();

            return Ok(places);
        }

    }
}
