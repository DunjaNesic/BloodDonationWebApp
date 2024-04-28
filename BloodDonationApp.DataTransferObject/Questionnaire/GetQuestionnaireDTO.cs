using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Questionnaire
{
    public class GetQuestionnaireDTO
    {
        public string QuestionnaireTitle { get; set; } = string.Empty;
        public string? Remark { get; set; }
        public List<Question> ListOfQuestions { get; set; } = new List<Question>();
    }
}
