using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.DonorCallsRepo
{
    public interface IDonorCallsRepository : IRepository<CallToDonate>
    {
        Task CreateCall(string JMBG, int actionID, bool accepted);
        Task<CallToDonate?> GetCall(string JMBG, int actionID, bool trackChanges);
        Task<IEnumerable<CallToDonate?>> GetAllCalls(string JMBG);
        Task<IEnumerable<CallToDonate?>> GetAACalls(string JMBG);
        Task<IEnumerable<CallToDonate?>> GetADCalls(string JMBG);
        Task<IEnumerable<CallToDonate?>> GetDDCalls(string JMBG);
        Task<IEnumerable<CallToDonate?>> GetDACalls(string JMBG);
    }
}
