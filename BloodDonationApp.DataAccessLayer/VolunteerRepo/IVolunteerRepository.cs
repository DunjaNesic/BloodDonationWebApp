using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using Common.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.VolunteerRepo
{
    public interface IVolunteerRepository : IRepository<Volunteer>
    {
        Task<Volunteer?> GetVolunteer(Expression<Func<Volunteer, bool>> condition, bool trackChanges);
        IQueryable<Volunteer> GetAllVolunteers(bool trackChanges, VolunteerParameters volunteerParameters);
        IQueryable<Volunteer> GetVolunteersByCondition(Expression<Func<Volunteer, bool>> condition, bool trackChanges);
        Task<IEnumerable<TransfusionAction>> GetActions(int volunteerID);
        Task<IEnumerable<TransfusionAction>> GetIncomingAction(int volunteerID, bool forNotifications);
        Task<IEnumerable<TransfusionAction>> GetVolunteersHistory(int volunteerID);
        Task<IEnumerable<Volunteer>> GetCalledVolunteersAsync(int actionID, bool v);
    }
}
