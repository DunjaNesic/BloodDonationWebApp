using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.DataTransferObject.Mappers;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Action;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.LoggerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Linq.Expressions;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class ActionService : IActionService
    {
        private readonly IUnitOfWork uow;
        private readonly ILoggerManager _logger;
        private readonly ActionMapper _mapper = new ActionMapper();
        public ActionService(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            uow = unitOfWork;
            _logger = logger;
        }
        public async Task<ApiBaseResponse> Delete(int actionID)
        {
            Expression<Func<TransfusionAction, bool>> condition = action => action.ActionID == actionID;
            var actionToDelete = await uow.ActionRepository.GetAction(actionID);
            if (actionToDelete == null) return new ActionNotFoundResponse();
            uow.ActionRepository.Delete(actionToDelete);
            await uow.SaveChanges();
            return new ApiOkResponse<string>("Action deleted successfully");
        }

        public async Task<ApiBaseResponse> GetAction(int actionID)
        {
            _logger.LogInformation("GetAction from ActionService");
            var action = await uow.ActionRepository.GetAction(actionID);
            if (action == null) return new ActionNotFoundResponse();
            var actionDTO = _mapper.ToDto(action);
            return new ApiOkResponse<GetTransfusionActionDTO>(actionDTO);
        }

        public async Task<ApiBaseResponse> GetAll(bool trackChanges)
        {
            _logger.LogInformation("GetAll from ActionService");
            var query = uow.ActionRepository.GetAllActions(trackChanges);
            var actions = await query.ToListAsync();
            var actionsDTO = actions.Select(a => _mapper.ToDto(a)).ToList();
            return new ApiOkResponse<IEnumerable<GetTransfusionActionDTO>>(actionsDTO);
        }

        public async Task<ApiBaseResponse> GetByCondition(Expression<Func<TransfusionAction, bool>> condition, bool trackChanges)
        {
            var query = uow.ActionRepository.GetActionsByCondition(condition, trackChanges);
            var actions = await query.ToListAsync();
            if (actions.IsNullOrEmpty()) return new ActionNotFoundResponse();
            var actionsDTO = actions.Select(a => _mapper.ToDto(a)).ToList();
            if (actionsDTO.IsNullOrEmpty()) return new ActionNotFoundResponse();
            return new ApiOkResponse<IEnumerable<GetTransfusionActionDTO>>(actionsDTO);
        }
    }
}
