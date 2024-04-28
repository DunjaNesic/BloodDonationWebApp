using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public class Questionnaire
    {
        public int QuestionnaireID { get; set; }
        public string JMBG { get; set; } = string.Empty;
        public Donor? Donor { get; set; }
        public int ActionID { get; set; }
        public TransfusionAction? TransfusionAction { get; set; }
        public string QuestionnaireTitle { get; set; } = string.Empty;
        public string? Remark { get; set; }
        public List<Question> ListOfQuestions { get; set; } = new List<Question>();
    }
}
