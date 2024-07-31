using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.UserRepo
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByEmailAsync(string email);
        Task<bool> CheckPasswordAsync(User user, string password);
        Task<IEnumerable<string>> GetRolesAsync(int userId);
        Task<Role?> FindRoleAsync(string roleName); 
        Task CreateUser(User user); 
    }
}
