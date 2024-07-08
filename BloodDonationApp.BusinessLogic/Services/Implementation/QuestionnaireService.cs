using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.DataTransferObject.Mappers;
using BloodDonationApp.DataTransferObject.Questionnaires;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Action;
using BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Donor;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using BloodDonationApp.LoggerService;
using Common.RequestFeatures;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class QuestionnaireService : IQuestionnaireService
    {
        private readonly IUnitOfWork uow;
        private readonly ILoggerManager _logger;
        private readonly QuestionnaireMapper _mapper = new QuestionnaireMapper();
        public QuestionnaireService(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            uow = unitOfWork;
            _logger = logger;
        }
        public async Task<ApiBaseResponse> GetAll(string JMBG, QuestionnaireParameters questionnaireParameters, bool trackChanges)
        {
            _logger.LogInformation("GetAll from QuestionnaireService");
            var foundDonor =  await uow.DonorRepository.GetByJMBG(JMBG);
            if (foundDonor == null) return new DonorNotFoundResponse(); 

            var questionnaires = await uow.QuestionnaireRepository.GetAllForDonorAsync(JMBG, questionnaireParameters, trackChanges);
            var questionnairesDTO = questionnaires.Select(q => _mapper.ToDto(q)).ToList();
            return new ApiOkResponse<IEnumerable<GetQuestionnaireDTO>>(questionnairesDTO);
        }
        public async Task<ApiBaseResponse> Create(string JMBG, int actionID, CreateQuestionnaireDTO questionnaireDTO)
        {
            var donor = await uow.DonorRepository.GetByJMBG(JMBG);
            if (donor == null) return new DonorNotFoundResponse();
            var action = await uow.ActionRepository.GetAction(actionID);
            if (action == null) return new ActionNotFoundResponse();

            var questions = uow.QuestionRepository.GetAll(false).ToList();

            var questionnaire = _mapper.FromDto(questionnaireDTO, questions);

            await uow.QuestionnaireRepository.CreateQuestionnaireForDonor(JMBG, actionID, questionnaire);
            await uow.SaveChanges();

            var questionnaireToReturn = _mapper.ToDto(questionnaire);

            return new ApiOkResponse<GetQuestionnaireDTO>(questionnaireToReturn);
        }


    }
}
