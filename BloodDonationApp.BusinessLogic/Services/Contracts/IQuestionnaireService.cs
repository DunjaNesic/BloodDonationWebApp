using BloodDonationApp.DataTransferObject.Questionnaires;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using Common.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Contracts
{
    public interface IQuestionnaireService
    {
        Task<ApiBaseResponse> GetAll(string JMBG, QuestionnaireParameters questionnaireParameters, bool trackChanges);
        Task<ApiBaseResponse> Create(string JMBG, int actionID, CreateQuestionnaireDTO questionnaireDTO);
        Task<ApiBaseResponse> Update(string JMBG, int actionID, UpdateQuestionnaireDTO questionnaireDTO);
    }
}
