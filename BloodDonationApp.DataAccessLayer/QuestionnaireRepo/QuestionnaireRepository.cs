using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using Common.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.QuestionnaireRepo
{
    public class QuestionnaireRepository : RepositoryBase<Questionnaire>, IQuestionnaireRepository
    {
        private readonly BloodDonationContext _context;
        public QuestionnaireRepository(BloodDonationContext context) : base(context) {
            _context = context;
        }
        public async Task CreateQuestionnaireForDonor(string JMBG, Questionnaire questionnaire)
        {
            questionnaire.JMBG = JMBG;
            await _context.Questionnaires.AddAsync(questionnaire);
        }

        public async Task<IEnumerable<Questionnaire>> GetAllForDonorAsync(string JMBG, QuestionnaireParameters questionnaireParameters, bool trackChanges)
        {
            var includes = new Expression<Func<Questionnaire, object>>[]
            {
                  q => q.ListOfQuestions,
                  q => q.Donor
            };

            var query = GetByCondition(q => q.JMBG.Equals(JMBG), trackChanges, includes);
            query = query.OrderBy(o => o.DateOfMaking)
                .Skip((questionnaireParameters.PageNumber-1)*questionnaireParameters.PageSize)
                .Take(questionnaireParameters.PageSize);

            return await query.ToListAsync();
        }
  
    }
}
