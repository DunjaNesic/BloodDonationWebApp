using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.DataTransferObject.Mappers;
using BloodDonationApp.Domain.CustomModel;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Action;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.LoggerService;
using Common.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Dynamic;
using System.Linq.Expressions;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class ActionService : IActionService
    {
        private readonly IUnitOfWork uow;
        private readonly ILoggerManager _logger;
        private readonly IDataShaper<GetTransfusionActionDTO> _dataShaper;
        private readonly ActionMapper _mapper = new ActionMapper();
        public ActionService(IUnitOfWork unitOfWork, ILoggerManager logger, IDataShaper<GetTransfusionActionDTO> dataShaper)
        {
            uow = unitOfWork;
            _logger = logger;
            _dataShaper = dataShaper;
        }
        public async Task<ApiBaseResponse> GetAll(bool trackChanges, ActionParameters actionParameters)
        {
            _logger.LogInformation("GetAll from ActionService");
            var query = uow.ActionRepository.GetAllActions(trackChanges, actionParameters);
            var actions = await query.ToListAsync();
            var actionsDTO = actions.Select(a => _mapper.ToDto(a)).ToList();
            var shapedActionsData = _dataShaper.ShapeData(actionsDTO, actionParameters.Fields);
            return new ApiOkResponse<IEnumerable<ShapedCustomExpando>>(shapedActionsData);
        }
        public async Task<ApiBaseResponse> GetAction(int actionID, string fields = "")
        {
            _logger.LogInformation("GetAction from ActionService");
            var action = await uow.ActionRepository.GetAction(actionID);
            if (action == null) return new ActionNotFoundResponse();
            var actionDTO = _mapper.ToDto(action);
            if (fields == "") return new ApiOkResponse<GetTransfusionActionDTO>(actionDTO);
            else
            {
                ShapedCustomExpando shapedAction = _dataShaper.ShapeData(actionDTO, fields);
                return new ApiOkResponse<ShapedCustomExpando>(shapedAction);
            }
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
