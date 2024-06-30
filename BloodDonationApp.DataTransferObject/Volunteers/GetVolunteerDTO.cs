using BloodDonationApp.DataTransferObject.Action;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Volunteers
{
    [Serializable]
    public class GetVolunteerDTO
    {
        public int VolunteerID { get; set; }
        public string VolunteerFullName { get; set; } = string.Empty;
        public DateTime DateFreeFrom { get; set; }
        public DateTime DateFreeTo { get; set; }
        public string PlaceName { get; set; } = string.Empty;
        public List<GetTransfusionActionDTO> ListOfActions { get; set; } = new List<GetTransfusionActionDTO> { };
    }
}
