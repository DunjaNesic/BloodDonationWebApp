using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.QuestionRepo
{
    public interface IQuestionRepository : IRepository<Question>
    {
        Task<IEnumerable<Question>> GetQuestionsByConditionAsync(Expression<Func<Question, bool>> condition, bool trackChanges);
        Task<IEnumerable<Question>> GetAllQuestions(bool trackChanges);

    }
}
