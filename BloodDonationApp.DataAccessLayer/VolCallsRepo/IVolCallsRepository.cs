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
        Task<IEnumerable<CallToVolunteer?>> GetAllCalls(int volunteerID);
        Task<IEnumerable<CallToVolunteer?>> GetAACalls(int volunteerID);
        Task<IEnumerable<CallToVolunteer?>> GetADCalls(int volunteerID);
        Task<IEnumerable<CallToVolunteer?>> GetDDCalls(int volunteerID);
        Task<IEnumerable<CallToVolunteer?>> GetDACalls(int volunteerID);
    }
}
