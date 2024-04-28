using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
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
    }
}
