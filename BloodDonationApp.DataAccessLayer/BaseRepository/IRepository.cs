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
        IQueryable<T> GetAll(bool trackChanges, params Expression<Func<T, object>>[] includes);
        IQueryable<T> GetByCondition(Expression<Func<T, bool>> condition, bool trackChanges, params Expression<Func<T, object>>[] includes);
        Task CreateAsync(T t);
        void Update(T t);
        void Delete(T t);

    }
}
