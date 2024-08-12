using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Action
{
    public class GetActionDetailsDTO
    {
        public int NumberOfAssignedOfficials { get; set; }
        public int NumberOfVolunteers { get; set; }
        public int NumberOfDonors { get; set; }
        public int MaleDonors { get; set; }
        public int FemaleDonors { get; set; }
        public int NewDonors { get; set; }
        public int OldDonors { get; set; }
        public DateTime[] TimeIntervals { get; set; } = new DateTime[0];
    }
}
