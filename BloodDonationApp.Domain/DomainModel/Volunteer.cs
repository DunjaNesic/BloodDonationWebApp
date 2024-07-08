using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public enum Sex
    {
        Musko = 0,
        Zensko = 1
    }
    public class Volunteer 
    {
        [Key]
        public int VolunteerID { get; set; }
        public string VolunteerFullName { get; set; } = string.Empty;
        public string VolunteerEmailAddress { get; set; } = string.Empty;
        //public required string Username { get; set; } 
        //public required string Password { get; set; } 
        public Sex Sex { get; set; }
        public DateTime DateFreeFrom { get; set; }
        public DateTime DateFreeTo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RedCrossID { get; set; }
        public RedCross RedCross { get; set; } = null!;

        [JsonIgnore]
        public List<TransfusionAction> ListOfActions { get; set; } = new List<TransfusionAction> { };
        public List<CallToVolunteer> CallsToVolunteer { get; set; } = new List<CallToVolunteer> { };
        public override string ToString()
        {
            return VolunteerFullName;
        }
    }
}
