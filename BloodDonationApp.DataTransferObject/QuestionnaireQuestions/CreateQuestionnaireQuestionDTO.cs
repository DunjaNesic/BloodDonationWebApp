using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.QuestionnaireQuestions
{
    [Serializable]
    public class CreateQuestionnaireQuestionDTO
    {
        public int QuestionID { get; set; }
        public bool Answer { get; set; } = false;
    }
}
