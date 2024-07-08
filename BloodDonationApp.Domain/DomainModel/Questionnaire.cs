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
    public class Questionnaire
    {
        public required string JMBG { get; set; }
        public Donor Donor { get; set; } = null!;
        public int ActionID { get; set; }
        public TransfusionAction Action { get; set; } = null!;
        public string? QuestionnaireTitle { get; set; }
        public bool Approved { get; set; }
        public string? Remark { get; set; }
        public DateTime DateOfMaking { get; set; }
        public List<Question>? Questions { get; set; }
        public List<QuestionnaireQuestion>? ListOfQuestions { get; set; }
        public string? QRCode { get; set; }
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
