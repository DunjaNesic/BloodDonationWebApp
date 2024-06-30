using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Linq.Expressions;


namespace BloodDonationApp.DataAccessLayer.VolunteerRepo
{
    public class VolunteerRepository : RepositoryBase<Volunteer>, IVolunteerRepository
    {
        private readonly BloodDonationContext _context;
        public VolunteerRepository(BloodDonationContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Volunteer> GetAllVolunteers(bool trackChanges)
        {
            var includes = new Expression<Func<Volunteer, object>>[]
          {
                 v => v.RedCross,
                 v => v.ListOfActions
          };
            var query = GetAll(trackChanges, includes);
            return query;
        }

        public IQueryable<Volunteer> GetVolunteersByCondition(Expression<Func<Volunteer, bool>> condition, bool trackChanges)
        {
            var includes = new Expression<Func<Volunteer, object>>[]
             {
                 v => v.RedCross,
                 v => v.ListOfActions
             };

            var query = GetByCondition(condition, trackChanges, includes);
   
            return query;
        }

        public async Task<Volunteer?> GetVolunteer(Expression<Func<Volunteer, bool>> condition, bool trackChanges)
        {
            var includes = new Expression<Func<Volunteer, object>>[]
             {
                 v => v.RedCross
             };

            var query = GetByCondition(condition, trackChanges, includes);

            var volunteer = await query.FirstOrDefaultAsync();
            return volunteer;
        }

        public async Task<IEnumerable<TransfusionAction>> GetActions(int volunteerID)
        {
            var volunteer = await _context.Volunteers
               .Include(d => d.CallsToVolunteer)
               .FirstOrDefaultAsync(d => d.VolunteerID == volunteerID);

            if (volunteer == null || volunteer.CallsToVolunteer == null || !volunteer.CallsToVolunteer.Any())
            {
                return Enumerable.Empty<TransfusionAction>();
            }

            var actionIds = volunteer.CallsToVolunteer.Select(ctv => ctv.ActionID).ToList();

            if (actionIds == null || !actionIds.Any())
            {
                return Enumerable.Empty<TransfusionAction>();
            }

            var actions = await _context.TransfusionActions
                .Where(a => actionIds.Contains(a.ActionID))
                .ToListAsync();

            return actions;
        }

        public async Task<IEnumerable<TransfusionAction>> GetIncomingAction(int volunteerID)
        {
            var volunteer = await _context.Volunteers
               .Include(d => d.CallsToVolunteer)
               .FirstOrDefaultAsync(d => d.VolunteerID == volunteerID);

            if (volunteer == null || volunteer.CallsToVolunteer == null || !volunteer.CallsToVolunteer.Any())
            {
                return Enumerable.Empty<TransfusionAction>();
            }

            var calls = volunteer.CallsToVolunteer.Where(ctv => ctv.AcceptedTheCall == true);

            var actionIds = calls.Select(ctv => ctv.ActionID).ToList();

            if (actionIds == null || !actionIds.Any())
            {
                return Enumerable.Empty<TransfusionAction>();
            }

            var actions = await _context.TransfusionActions
                .Where(a => actionIds.Contains(a.ActionID) && a.ActionDate > DateTime.UtcNow)
                .ToListAsync();

            return actions;
        }
    }
}
