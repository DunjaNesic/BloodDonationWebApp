using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.BusinessLogic.Services.Implementation
{
    public class QuestionService : IQuestionService
    {
        private readonly IUnitOfWork uow;
        public QuestionService(IUnitOfWork unitOfWork)
        {
            uow = unitOfWork;
        }

        public async Task<ApiBaseResponse> GetAllQuestions(bool trackChanges)
        {
            var questions = await uow.QuestionRepository.GetAllQuestions(false);
            return new ApiOkResponse<IEnumerable<Question>>(questions);
        }

        public async Task<ApiBaseResponse> GetQuestionsForDonor(bool trackChanges)
        {
            Expression<Func<Question, bool>> condition = question => question.Flag == 0;
            var questions = await uow.QuestionRepository.GetQuestionsByConditionAsync(condition, false);
            return new ApiOkResponse<IEnumerable<Question>>(questions);
        }
    }
}
