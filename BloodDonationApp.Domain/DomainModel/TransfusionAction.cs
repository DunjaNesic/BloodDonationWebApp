using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    [DataContract(IsReference = true)]

    public class TransfusionAction
    {
        [Key]
        public int ActionID { get; set; }
        public required string ActionName { get; set; }
        public DateTime ActionDate { get; set; }
        public string? ActionTimeFromTo { get; set; }
        public string? ExactLocation { get; set; }
        public int PlaceID { get; set; }
        public Place Place { get; set; } = null!;
        public int OfficialID { get; set; }
        public Official ActionCoordinator { get; set; } = null!;
        public List<Volunteer>? ListOfVolunteers { get; set; } = new List<Volunteer>();
        public List<CallToVolunteer> ListOfCallsToVolunteers { get; set; } = new List<CallToVolunteer>();
        public List<Donor>? ListOfDonors { get; set; } = new List<Donor>();
        public List<CallToDonate> ListOfCallsToDonors { get; set; } = new List<CallToDonate>();
        public List<Questionnaire>? ListOfQuestionnaires { get; set; } = new List<Questionnaire>();
        public List<Official>? ListOfActionOfficials { get; set; }
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
        public override string ToString()
        {
            return ActionName;
        }
    }
}
