using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.Domain.CustomModel;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.LoggerService;
using Common.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class PlaceService : IPlaceService
    {
        private readonly IUnitOfWork uow;
        private readonly ILoggerManager _logger;
        public PlaceService(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            uow = unitOfWork;
            _logger = logger;
        }
        public async Task<ApiBaseResponse> GetAll(bool trackChanges)
        {
            _logger.LogInformation("GetAll from PlaceService");
            var places = await uow.PlaceRepository.GetAll(trackChanges).ToListAsync();
            return new ApiOkResponse<IEnumerable<Place>>(places);
        }
    }
}
