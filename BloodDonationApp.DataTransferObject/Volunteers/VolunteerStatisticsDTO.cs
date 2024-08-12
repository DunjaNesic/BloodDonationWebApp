using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Volunteers
{
    public class VolunteerStatisticsDTO
    {
        public int VolunteerID { get; set; }
        public string? FullName { get; set; }
        public int TotalActions { get; set; }
        public double AcceptedAndAttendedPercentage { get; set; }
        public double AcceptedButDidNotAttendPercentage { get; set; }
        public double DeclinedAndDidNotAttendPercentage { get; set; }
        public double DeclinedButAttendedPercentage { get; set; }
    }
}
