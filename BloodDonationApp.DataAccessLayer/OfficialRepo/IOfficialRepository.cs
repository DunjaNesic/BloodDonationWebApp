using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.OfficialRepo
{
    public interface IOfficialRepository : IRepository<Official>
    {
        Task<Official?> GetByUserID(int userID);
        Task<Official?> GetOfficial(int officialID);
    }
}
