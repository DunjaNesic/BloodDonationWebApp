using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Questionnaires
{
    [Serializable]
    public class GetQuestionnaireDTO
    {
        public string? QuestionnaireTitle { get; set; }
        public string? Remark { get; set; }
        public List<QuestionnaireQuestionDTO>? AnsweredQuestions { get; set; }

    }
}
