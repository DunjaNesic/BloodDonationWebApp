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
    public enum IsActive
    {
        Ne = 0,
        Da = 1
    }
    public class Donor
    {
        [Key]
        public string JMBG { get; set; } = string.Empty;
        public string DonorFullName { get; set; } = string.Empty;
        public string DonorEmailAddress { get; set; } = string.Empty;
        public BloodType BloodType { get; set; }
        public IsActive IsActive { get; set; }
        public DateTime LastDonationDate { get; set; }
        public int PlaceID { get; set; }
        public Place? Place { get; set; }

        [JsonIgnore]
        public List<TransfusionAction>? ListOfActions { get; set; }
        public List<Questionnaire>? ListOfQuestionnaires { get; set; }
        public byte[] RowVersion { get; set; } = new byte[0];
        public override string ToString()
        {
            return DonorFullName;
        }
    }
}
