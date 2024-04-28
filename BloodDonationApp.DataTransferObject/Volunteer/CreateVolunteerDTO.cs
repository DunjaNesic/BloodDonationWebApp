using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Volunteer
{
    public class CreateVolunteerDTO
    {
        public string VolunteerFullName { get; set; } = string.Empty;
        public string VolunteerEmailAddress { get; set; } = string.Empty;
        public DateTime DateFreeFrom { get; set; }
        public DateTime DateFreeTo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int PlaceID { get; set; }
    }
}
