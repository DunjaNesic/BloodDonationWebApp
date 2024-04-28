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
        public Sex Sex { get; set; }
        public DateTime DateFreeFrom { get; set; }
        public DateTime DateFreeTo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PlaceID { get; set; }
        public Place? Place { get; set; }
        public string VolunteerEmailAddress { get; set; } = string.Empty;
        public byte[] RowVersion { get; set; } = new byte[0];

        [JsonIgnore]
        public List<TransfusionAction>? ListOfActions { get; set; }
        public override string ToString()
        {
            return VolunteerFullName;
        }
    }
}
