using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Volunteers
{
    public class CreateVolunteerCallsDTO
    {
        public int[]? VolunteerIDs { get; set; }
        public int ActionID { get; set; }
    }
}
