using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using Common.RequestFeatures;
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
        IQueryable<Donor> GetAllDonors(bool trackChanges, DonorParameters donorParameters);
        Task<Donor?> GetByJMBG(string JMBG);
        Task<IEnumerable<TransfusionAction>> GetIncomingAction(string jMBG, bool forNotifications);
        IQueryable<Donor> GetDonorsByCondition(Expression<Func<Donor, bool>> condition, bool trackChanges);
        Task<IEnumerable<TransfusionAction>> GetDonorsHistory(string JMBG);
    }
}
