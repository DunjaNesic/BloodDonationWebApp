using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public class RedCrossWorker : Official
    {
        public int RedCrossID { get; set; }
        public RedCross? RedCross { get; set; }
    }
}
