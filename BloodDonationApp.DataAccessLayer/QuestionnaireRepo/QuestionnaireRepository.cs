using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.QuestionnaireRepo
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly BloodDonationContext _context;
        public QuestionnaireRepository(BloodDonationContext context)
        {
            _context = context;
        }

        public Task CreateAsync(Questionnaire t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Questionnaire t)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Questionnaire>> GetAllAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Questionnaire>> GetByConditionAsync(Expression<Func<Questionnaire, bool>> condition, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Questionnaire t)
        {
            throw new NotImplementedException();
        }
    }
}
