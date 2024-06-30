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
        public string? QuestionText { get; set; }
        public int Flag { get; set; }
    }
}
