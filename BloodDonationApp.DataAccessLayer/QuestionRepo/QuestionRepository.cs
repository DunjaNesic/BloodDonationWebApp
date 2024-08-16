using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.QuestionRepo
{
    public class QuestionRepository : RepositoryBase<Question>, IQuestionRepository
    {
        private readonly BloodDonationContext _context;
        public QuestionRepository(BloodDonationContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<IEnumerable<Question>> GetQuestionsByConditionAsync(Expression<Func<Question, bool>> condition, bool trackChanges)
        {
            return await GetByCondition(condition, trackChanges).ToListAsync();
        }

        public async Task<IEnumerable<Question>> GetAllQuestions(bool trackChanges)
        {
            return await GetAll(trackChanges).ToListAsync();
        }
    }
}
