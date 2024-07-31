using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Donors
{
    public class DonorStatisticsDTO
    {
        public string JMBG { get; set; } = string.Empty;
        public int TotalActions { get; set; } 
        public double AcceptedAndAttendedPercentage { get; set; } 
        public double AcceptedButDidNotAttendPercentage { get; set; } 
        public double DeclinedAndDidNotAttendPercentage { get; set; } 
        public double DeclinedButAttendedPercentage { get; set; } 
    }
}
