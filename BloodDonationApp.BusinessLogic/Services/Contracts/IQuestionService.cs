using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Contracts
{
    public interface IQuestionService
    {
        Task<ApiBaseResponse> GetQuestionsForDonor(bool trackChanges);
        Task<ApiBaseResponse> GetAllQuestions(bool trackChanges);

    }
}
