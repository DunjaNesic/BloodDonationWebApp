
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public class Role
    {
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; } = new List<User>();
    }
}
