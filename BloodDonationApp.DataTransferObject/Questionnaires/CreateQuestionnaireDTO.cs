using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Questionnaires
{
    [Serializable]
    public class CreateQuestionnaireDTO
    {
        public string? QuestionnaireTitle { get; set; }
        public string? Remark { get; set; }
        public DateTime DateOfMaking { get; set; } = DateTime.UtcNow;
        public List<bool> Answers { get; set; } = new List<bool>();
    }
}
