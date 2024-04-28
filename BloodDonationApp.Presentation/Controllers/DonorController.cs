using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataTransferObject.Donor;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.Presentation.Mappers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Presentation.Controllers
{
    [ApiController]
    [Route("api/donors")]
    public class DonorController : ApiBaseController
    {
        private readonly IServiceManager _serviceManager;
        public DonorController(IServiceManager serviceManager)
        {
            _serviceManager = serviceManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetDonorDTO>>> GetAllDonorsAsync()
        {
            var baseResult = await _serviceManager.DonorService.GetAll(trackChanges: false);
            var donors = ((ApiOkResponse<IQueryable<Donor>>)baseResult).Result;
            return Ok(donors.Select(DonorMapper.ToDto).ToList());
        }


        [HttpGet]
        [Route("{JMBG}")]
        public async Task<ActionResult<IQueryable<GetDonorDTO>>> FindDonorByJMBGAsync(string JMBG)
        {
            Expression<Func<Donor, bool>> condition = d => d.JMBG.ToLower().Equals(JMBG.ToLower());
            var baseResult = await _serviceManager.DonorService.GetByCondition(condition, false);
            if (!baseResult.Success) return ProcessError(baseResult);

            var foundDonor = baseResult.GetResult<IQueryable<Donor>>();
            return Ok(foundDonor.Select(DonorMapper.ToDto).ToList());
        }
    }
}
