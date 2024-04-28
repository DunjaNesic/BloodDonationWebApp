using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Action
{
    public class GetTransfusionActionDTO
    {
        public string ActionName { get; set; } = string.Empty;
        public DateTime ActionDate { get; set; }
        public string ActionTimeFromTo { get; set; } = string.Empty;
    }
}
