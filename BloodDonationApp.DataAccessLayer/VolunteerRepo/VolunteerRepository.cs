using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace BloodDonationApp.DataAccessLayer.VolunteerRepo
{
    public class VolunteerRepository : IVolunteerRepository
    {
        private readonly BloodDonationContext _context;
        public VolunteerRepository(BloodDonationContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(Volunteer v)
        {
            await _context.Volunteers.AddAsync(v);
        }

        public Task DeleteAsync(Volunteer v)
        {
            _context.Volunteers.Remove(v);      
            return Task.CompletedTask;
        }

        public async Task<IQueryable<Volunteer>> GetAllAsync(bool trackChanges)
        {            
            IQueryable<Volunteer> query = _context.Volunteers
            .Include(v => v.Place)
            .Include(v => v.ListOfActions);

            query = trackChanges ? query : query.AsNoTracking();

            return await Task.FromResult(query);
        }

        public async Task<IQueryable<Volunteer>> GetByConditionAsync(Expression<Func<Volunteer, bool>> condition, bool trackChanges)
        {
            var query = _context.Volunteers
            .Include(v => v.Place)
            .Where(condition)
            .AsQueryable();

            query = trackChanges ? query : query.AsNoTracking();

            return await Task.FromResult(query);
        }

        public async Task<Volunteer?> GetVolunteer(Expression<Func<Volunteer, bool>> condition, bool trackChanges)
        {
            var query = _context.Volunteers
            .Where(condition)
            .AsQueryable();

            query = trackChanges ? query : query.AsNoTracking();

            var volunteer = await query.FirstOrDefaultAsync();
            return volunteer;
        }

        public Task UpdateAsync(Volunteer v)
        {
            //nesto ovde ne radim lepo
            _context.Entry(v).State = EntityState.Modified;
            _context.Volunteers.Update(v);
            return Task.CompletedTask;
        }

   
    }
}
