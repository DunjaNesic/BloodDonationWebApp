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

        //public async Task<IQueryable<Question>> GetAllAsync(bool trackChanges)
        //{
        //    IEnumerable<Question> query = await _context.Questions.ToListAsync();


        //    IQueryable<Question> result = query.AsQueryable();

        //    result = trackChanges ? result : result.AsNoTracking();

        //    return result;
        //}

        //IQueryable<Question> GetAll(bool trackChanges)
        //{
        //     IEnumerable<Question> query = _context.Questions.ToList(); 

        //     IQueryable<Question> result = query.AsQueryable();

        //     result = trackChanges ? result : result.AsNoTracking();

        //     return result;
        //}

    }
}
