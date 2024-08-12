using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Officials
{
    public class GetOfficialDTO
    {
        public int OfficialID { get; set; }
        public string OfficialFullName { get; set; } = string.Empty;
        public int UserID { get; set; }
    }
}
