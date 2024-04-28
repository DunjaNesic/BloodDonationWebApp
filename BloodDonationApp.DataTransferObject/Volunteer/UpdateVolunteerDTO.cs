using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Volunteer
{
    public class UpdateVolunteerDTO
    {
        public byte[]? RowVersion { get; set; }
        public string? VolunteerFullName { get; set; }
        public string? VolunteerEmailAddress { get; set; }
        public int PlaceID { get; set; }
        public DateTime DateFreeFrom { get; set; }
        public DateTime DateFreeTo { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
