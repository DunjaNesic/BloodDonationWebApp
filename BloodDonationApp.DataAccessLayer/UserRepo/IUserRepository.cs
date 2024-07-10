using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.UserRepo
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<User?> FindByEmailAsync(string email);
        public Task<bool> CheckPasswordAsync(User user, string password);
        Task<IEnumerable<string>> GetRolesAsync(int userId);
    }
}
