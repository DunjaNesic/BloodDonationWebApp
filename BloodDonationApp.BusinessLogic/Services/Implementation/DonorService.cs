using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.DataTransferObject.Mappers;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Action;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Donor;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.LoggerService;
using Common.RequestFeatures;
using Microsoft.EntityFrameworkCore;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class DonorService : IDonorService
    {
        private readonly IUnitOfWork uow;
        private readonly ILoggerManager _logger;
        private readonly DonorMapper _mapper = new DonorMapper();
        private readonly ActionMapper _actionMapper = new ActionMapper();
        private readonly CallToDonateMapper _callsMapper = new CallToDonateMapper();
        public DonorService(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            uow = unitOfWork;   
            _logger = logger;
        }
        public async Task<ApiBaseResponse> GetAll(bool trackChanges, DonorParameters donorParameters)
        {
            _logger.LogInformation("GetAll from DonorService");
            var query = uow.DonorRepository.GetAllDonors(trackChanges, donorParameters);
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
            _logger.LogInformation("GetIncomingDonorAction from DonorService");
            var actions = await uow.DonorRepository.GetIncomingAction(jMBG, false);

            if (!actions.Any())
            {
                return new ActionNotFoundResponse();
            }
            var actionsDTO = actions.Select(a => _actionMapper.ToDto(a)).ToList();
            return new ApiOkResponse<IEnumerable<GetTransfusionActionDTO>>(actionsDTO);
        }

        public async Task<ApiBaseResponse> CallDonor(string JMBG, int actionID)
        {
            var donor = await uow.DonorRepository.GetByJMBG(JMBG);
            if (donor == null) return new DonorNotFoundResponse();

            var action = await uow.ActionRepository.GetAction(actionID);
            if (action == null) return new ActionNotFoundResponse();

            bool accepted = true;

            try
            {
                await uow.DonorCallsRepository.CreateCall(JMBG, actionID, accepted);
                await uow.SaveChanges();
                return new ApiOkResponse<string>("woo");
            }
            catch (InvalidOperationException ex)
            {
                return new DonorLegalReasons("Nije bas ok prijavi - odjavi ali ok");
            }
        }


        public async Task<ApiBaseResponse> UpdateCallToDonor(string JMBG, int actionID, CallsToDonorDTO donorCall)
        {
            var donor = await uow.DonorRepository.GetByJMBG(JMBG);
            if (donor == null) return new DonorNotFoundResponse();

            var action = await uow.ActionRepository.GetAction(actionID);
            if (action == null) return new ActionNotFoundResponse();

            var call = await uow.DonorCallsRepository.GetCall(JMBG, actionID, true);
            if (call == null) return new DonorNotFoundResponse();

            if (call.AcceptedTheCall == true && call.ShowedUp == false && donorCall.AcceptedTheCall == true && donorCall.ShowedUp == false)
            {
                return new ApiOkResponse<string>("Vec ste prijavljeni na ovu akciju");
            }

            call.AcceptedTheCall = donorCall.AcceptedTheCall;
            call.ShowedUp = donorCall.ShowedUp;

            await uow.SaveChanges();
            return new ApiOkResponse<string>("Uspesno azuriran poziv na akciju");
        }

        public async Task<ApiBaseResponse> GetDonorsNotifications(string JMBG, bool history)
        {
            IEnumerable<TransfusionAction> actions;

            if (!history)
            {
                actions = await uow.DonorRepository.GetIncomingAction(JMBG, true);
            }
            else {
                actions = await uow.DonorRepository.GetDonorsHistory(JMBG);
            }
            if (!actions.Any())
            {
                return new ActionNotFoundResponse();
            }
            var actionsDTO = actions.Select(a => _actionMapper.ToDto(a)).ToList();
            return new ApiOkResponse<IEnumerable<GetTransfusionActionDTO>>(actionsDTO);
        }

        public async Task<ApiBaseResponse> GetDonorStats(GetDonorDTO donor)
        {
            var allCalls = await uow.DonorCallsRepository.GetAllCalls(donor.JMBG);
            if (!allCalls.Any())
            {
                return new DonorNotFoundResponse();
            }

            var acceptedAndAttendedCalls = await uow.DonorCallsRepository.GetAACalls(donor.JMBG);
            var acceptedButNotAttendedCalls = await uow.DonorCallsRepository.GetADCalls(donor.JMBG);
            var declinedAndNotAttendedCalls = await uow.DonorCallsRepository.GetDDCalls(donor.JMBG);
            var declinedButAttendedCalls = await uow.DonorCallsRepository.GetDACalls(donor.JMBG);

            int totalActions = allCalls.Count();
            int acceptedAndAttended = acceptedAndAttendedCalls.Count();
            int acceptedButNotAttended = acceptedButNotAttendedCalls.Count();
            int declinedAndNotAttended = declinedAndNotAttendedCalls.Count();
            int declinedButAttended = declinedButAttendedCalls.Count();

            DonorStatisticsDTO donorStats = new DonorStatisticsDTO
            {
                JMBG = donor.JMBG,
                FullName = donor.DonorFullName,
                TotalActions = totalActions,
                AcceptedAndAttendedPercentage = (double)acceptedAndAttended / totalActions * 100,
                AcceptedButDidNotAttendPercentage = (double)acceptedButNotAttended / totalActions * 100,
                DeclinedAndDidNotAttendPercentage = (double)declinedAndNotAttended / totalActions * 100,
                DeclinedButAttendedPercentage = (double)declinedButAttended / totalActions * 100,
            };

            return new ApiOkResponse<DonorStatisticsDTO>(donorStats);
        }
    }
}
