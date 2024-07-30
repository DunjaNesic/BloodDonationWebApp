using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Action
{
    [Serializable]
    public class GetTransfusionActionDTO
    {
        public int ActionID { get; set; }
        public string ActionName { get; set; } = string.Empty;
        public DateTime ActionDate { get; set; }
        public string ActionTimeFromTo { get; set; } = string.Empty;
        public string? ExactLocation { get; set; }
        public string? PlaceName { get; set; }
    }
}
