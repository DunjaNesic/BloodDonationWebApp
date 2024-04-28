using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.DomainModel
{
    public class Question
    {
        [Key]
        public int QuestionID { get; set; }
        public Questionnaire? Questionnaire { get; set; }
        public string JMBG { get; set; } = string.Empty;
        public int ActionID { get; set; }
        public int QuestionnaireID { get; set; }
        public string? QuestionText { get; set; }
        public bool Answer { get; set; }
    }
}
