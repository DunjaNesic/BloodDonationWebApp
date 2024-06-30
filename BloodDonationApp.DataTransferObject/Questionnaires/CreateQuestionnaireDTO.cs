﻿using BloodDonationApp.DataTransferObject.QuestionnaireQuestions;
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
        public required string JMBG { get; set; }
        public int ActionID { get; set; }
        public string? QuestionnaireTitle { get; set; }
        public string? Remark { get; set; }
        public DateTime DateOfMaking { get; set; }
        public List<CreateQuestionnaireQuestionDTO> ListOfQuestions { get; set; } = new List<CreateQuestionnaireQuestionDTO>();
    }
}
