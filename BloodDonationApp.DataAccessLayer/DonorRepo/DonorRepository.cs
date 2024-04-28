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

namespace BloodDonationApp.DataAccessLayer.DonorRepo
{
    public class DonorRepository : IDonorRepository
    {
        private readonly BloodDonationContext _context;
        public DonorRepository(BloodDonationContext context)
        {
            _context = context;
        }
        public Task CreateAsync(Donor t)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(Donor t)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<Donor>> GetAllAsync(bool trackChanges)
        {
            IQueryable<Donor> query = _context.Donors
                .Include(d => d.Place);

            query = trackChanges ? query : query.AsNoTracking();

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<Donor>> GetByConditionAsync(Expression<Func<Donor, bool>> condition, bool trackChanges)
        {
            var query = _context.Donors
           .Include(d => d.Place)
           .Where(condition)
           .AsQueryable();

            query = trackChanges ? query : query.AsNoTracking();

            return await Task.FromResult(query);
        }
        public Task UpdateAsync(Donor t)
        {
            throw new NotImplementedException();
        }
    }
}
