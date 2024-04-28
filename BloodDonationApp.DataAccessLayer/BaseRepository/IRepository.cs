using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.BaseRepository
{
    public interface IRepository<T> where T : class
    {
        Task<IQueryable<T>> GetAllAsync(bool trackChanges);
        Task<IQueryable<T>> GetByConditionAsync(Expression<Func<T, bool>> condition, bool trackChanges);
        Task CreateAsync(T t);
        Task UpdateAsync(T t);
        Task DeleteAsync(T t);

    }
}
