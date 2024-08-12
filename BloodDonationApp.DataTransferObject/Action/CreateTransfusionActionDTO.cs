using BloodDonationApp.DataTransferObject.Donors;
using BloodDonationApp.DataTransferObject.Officials;
using BloodDonationApp.DataTransferObject.Volunteers;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Action
{
    public class CreateTransfusionActionDTO
    {
        public required string ActionName { get; set; }
        public DateTime ActionDate { get; set; }
        public string? ActionTimeFromTo { get; set; }
        public string? ExactLocation { get; set; }
        public int PlaceID { get; set; }
        public int OfficialID { get; set; }
        public List<int>? ListOfVolunteerIDs { get; set; }
        public List<string>? ListOfDonorIDs { get; set; }
        public List<int>? ListOfActionOfficialIDs { get; set; }

    }
}
