using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.DataTransferObject.Mappers;
using BloodDonationApp.DataTransferObject.Officials;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Official;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.LoggerService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class OfficialService : IOfficialService
    {
        private readonly IUnitOfWork uow;
        private readonly ILoggerManager _logger;
        private readonly OfficialMapper _mapper = new OfficialMapper();

        public OfficialService(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            uow = unitOfWork;
            _logger = logger;
        }

        public async Task<ApiBaseResponse> GetAll(int officialsID)
        {
            _logger.LogInformation("GetAll from OfficialService");

            var officials = await uow.OfficialRepository.GetByCondition(o => o.OfficialID != officialsID, false).ToListAsync();

            var officialsToReturn = officials.Select(o => _mapper.ToDto(o));

            return new ApiOkResponse<IEnumerable<GetOfficialDTO>>(officialsToReturn);
        }


        public async Task<ApiBaseResponse> GetOfficial(int userID)
        {
            _logger.LogInformation("GetOfficial from OfficialService");
            var official = await uow.OfficialRepository.GetByUserID(userID);
            if (official == null) return new OfficialNotFoundResponse();
            var officialDTO = _mapper.ToDto(official);
            return new ApiOkResponse<GetOfficialDTO>(officialDTO);
        }
    }
}
