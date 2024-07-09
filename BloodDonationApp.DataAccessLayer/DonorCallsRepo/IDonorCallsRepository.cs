using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.DonorCallsRepo
{
    public interface IDonorCallsRepository : IRepository<CallToDonate>
    {
        Task CreateCall(string JMBG, int actionID, bool accepted);
        Task<CallToDonate?> GetCall(string JMBG, int actionID, bool trackChanges);
    }
}
