using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public class Official
    {
        [Key]
        public int OfficialID { get; set; }
        public string OfficialFullName { get; set; } = string.Empty;
        public required string Username { get; set; } 
        public required string Password { get; set; } 
        public List<TransfusionAction>? ListOfActions { get; set; }
        public List<TransfusionAction>? CreatedActions { get; set; }
    }
}
