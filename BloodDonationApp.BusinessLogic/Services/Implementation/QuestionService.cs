using BloodDonationApp.BusinessLogic.Services.Contracts;
using BloodDonationApp.DataAccessLayer.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
