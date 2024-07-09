using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.VolCallsRepo
{
    public interface IVolCallsRepository : IRepository<CallToVolunteer>
    {
        Task CreateCall(int volunteerID, int actionID, bool accepted);
        Task<CallToVolunteer?> GetCall(int volunteerID, int actionID, bool trackChanges);
    }
}
