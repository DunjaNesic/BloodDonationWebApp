using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.DonorRepo
{
    public interface IDonorRepository : IRepository<Donor>
    {
        Task<IEnumerable<TransfusionAction>> GetActions(string jMBG);
        IQueryable<Donor> GetAllDonors(bool trackChanges);
        Task<Donor?> GetByJMBG(string JMBG);
        Task<IEnumerable<TransfusionAction>> GetIncomingAction(string jMBG);
    }
}
