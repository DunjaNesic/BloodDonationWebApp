using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Donors
{
    public class CreateDonorCallsDTO
    {
        public string[]? JMBGs { get; set; }
        public int ActionID { get; set; }
    }
}
