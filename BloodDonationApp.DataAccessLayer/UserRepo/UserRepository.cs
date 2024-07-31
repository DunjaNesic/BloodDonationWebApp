using BloodDonationApp.DataAccessLayer.BaseRepository;
using BloodDonationApp.Domain.DomainModel;
using BloodDonationApp.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataAccessLayer.UserRepo
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        private readonly BloodDonationContext _context;
        public UserRepository(BloodDonationContext context) : base(context)
        {
            _context = context;
        }

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string password, string passwordHash)
        {
            return BCrypt.Net.BCrypt.Verify(password, passwordHash);
        }

        public async Task<User?> FindByEmailAsync(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).SingleOrDefaultAsync();
            return user;
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await Task.Run(() => VerifyPassword(password, user.Password));
        }

        public async Task<IEnumerable<string>> GetRolesAsync(int userId)
        {
            var user = await _context.Users.Include(u => u.Roles)
                                      .FirstOrDefaultAsync(u => u.UserID == userId);
            if (user == null)
            {
                return new List<string>();
            }

            return user.Roles.Select(r => r.RoleName).ToList();
        }

        public async Task<Role?> FindRoleAsync(string roleName)
        {
            return await _context.Roles.SingleOrDefaultAsync(r => r.RoleID == roleName);
        }

        public async Task CreateUser(User user)
        {
            await CreateAsync(user);
        }
    }
}
