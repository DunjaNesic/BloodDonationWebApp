using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    [Serializable]
    public class QuestionnaireQuestion
    {
        public int QuestionID { get; set; }
        public Question Question { get; set; } = null!;
        public Questionnaire Questionnaire { get; set; } = null!;
        public bool Answer { get; set; }
        public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    }
}
