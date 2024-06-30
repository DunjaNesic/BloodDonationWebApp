using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.DataTransferObject.Mappers;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Action;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Donor;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.LoggerService;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class DonorService : IDonorService
    {
        private readonly IUnitOfWork uow;
        private readonly ILoggerManager _logger;
        private readonly DonorMapper _mapper = new DonorMapper();
        private readonly ActionMapper _actionMapper = new ActionMapper();
        public DonorService(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            uow = unitOfWork;   
            _logger = logger;
        }
        public async Task<ApiBaseResponse> GetAll(bool trackChanges)
        {
            _logger.LogInformation("GetAll from DonorService");
            var query = uow.DonorRepository.GetAllDonors(trackChanges);
            var donors = await query.ToListAsync();
            var donorsDTO = donors.Select(donor => _mapper.ToDto(donor)).ToList();
            return new ApiOkResponse<IEnumerable<GetDonorDTO>>(donorsDTO);
        }
        public async Task<ApiBaseResponse> GetByCondition(string JMBG)
        {
            var foundDonor = await uow.DonorRepository.GetByJMBG(JMBG);
            if (foundDonor == null) return new DonorNotFoundResponse();
            var foundDonorDTO = _mapper.ToDto(foundDonor);
            if (foundDonorDTO == null) return new DonorNotFoundResponse();
            return new ApiOkResponse<GetDonorDTO>(foundDonorDTO);
        }

        public async Task<ApiBaseResponse> GetDonorsActions(string jMBG)
        {
            var actions = await uow.DonorRepository.GetActions(jMBG);

            if (!actions.Any())
            {
                return new ActionNotFoundResponse();
            }
            var actionsDTO = actions.Select(a => _actionMapper.ToDto(a)).ToList();
            return new ApiOkResponse<IEnumerable<GetTransfusionActionDTO>>(actionsDTO);
        }

        public async Task<ApiBaseResponse> GetIncomingDonorAction(string jMBG)
        {
            var actions = await uow.DonorRepository.GetIncomingAction(jMBG);

            if (!actions.Any())
            {
                return new ActionNotFoundResponse();
            }
            var actionsDTO = actions.Select(a => _actionMapper.ToDto(a)).ToList();
            return new ApiOkResponse<IEnumerable<GetTransfusionActionDTO>>(actionsDTO);
        }
    }
}
