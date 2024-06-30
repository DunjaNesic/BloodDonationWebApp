using BloodDonationApp.DataTransferObject.QuestionnaireQuestions;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Mappers
{
    public static class QuestionnaireQuestionMapper
    {
        public static QuestionnaireQuestion FromDto(this CreateQuestionnaireQuestionDTO dto) => new (){
            Answer = dto.Answer,        
            QuestionID = dto.QuestionID
        };
    }
}
