using BloodDonationApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.BaseRepository
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly BloodDonationContext _context;
        public RepositoryBase(BloodDonationContext context)
        {
            _context = context;
        }

        public IQueryable<T> GetAll(bool trackChanges, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = trackChanges ? _context.Set<T>() : _context.Set<T>().AsNoTracking();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }
            return query.AsSplitQuery();
        }

        public IQueryable<T> GetByCondition(Expression<Func<T, bool>> condition, bool trackChanges, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = _context.Set<T>();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            query = query.Where(condition).AsSplitQuery();
            return trackChanges ? query : query.AsNoTracking();
        }
        public async Task CreateAsync(T t)
        {
           await _context.AddAsync(t);
        }
        public void Delete(T t)
        {
            _context.Remove(t);
        }
        public void Update(T t)
        {
           _context.Update(t);
        }
    }
}
