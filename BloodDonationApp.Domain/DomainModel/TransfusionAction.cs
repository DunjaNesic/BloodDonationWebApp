using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public class TransfusionAction
    {
        [Key]
        public int ActionID { get; set; }
        public string ActionName { get; set; } = string.Empty;
        public DateTime ActionDate { get; set; }
        public string ActionTimeFromTo { get; set; } = string.Empty;
        public string ExactLocation { get; set; } = string.Empty;
        public Place? Place { get; set; }
        public int PlaceID { get; set; }
        public List<Volunteer>? ListOfVolunteers { get; set; }
        public List<Donor>? ListOfDonors { get; set; }
        public List<Questionnaire>? ListOfQuestionnaires { get; set; }
        public override string ToString()
        {
            return ActionName;
        }
    }
}
