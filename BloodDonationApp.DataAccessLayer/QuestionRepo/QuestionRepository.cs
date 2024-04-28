using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.QuestionRepo
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly BloodDonationContext _context;
        public QuestionRepository(BloodDonationContext context)
        {
            _context = context;
        }
        public Task CreateAsync(Question t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Question t)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Question>> GetAllAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<Question>> GetByConditionAsync(Expression<Func<Question, bool>> condition, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Question t)
        {
            throw new NotImplementedException();
        }
    }
}
