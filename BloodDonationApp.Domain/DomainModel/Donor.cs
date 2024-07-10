using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public enum BloodType
    {
        APozitivna,
        ANegativna,
        BPozitivna,
        BNegativna,
        ABPozitivna,
        ABNegativna,
        OPozitivna,
        ONegativna
    }
    public class Donor 
    {
        [Key]
        public required string JMBG { get; set; }
        public required string DonorFullName { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public Sex Sex { get; set; }
        public BloodType BloodType { get; set; }
        public bool IsActive { get; set; }
        public DateTime LastDonationDate { get; set; }
        public int PlaceID { get; set; }
        public Place Place { get; set; } = null!;

        [JsonIgnore]
        public List<TransfusionAction> ListOfActions { get; set; } = new List<TransfusionAction> { };
        public List<CallToDonate> CallsToDonate { get; set; } = new List<CallToDonate> { };
        public List<Questionnaire> ListOfQuestionnaires { get; set; } = new List<Questionnaire> { };
        public override string ToString()
        {
            return DonorFullName;
        }
    }
}
