using BloodDonationApp.DataTransferObject;
using BloodDonationApp.DataTransferObject.Questionnaires;
using BloodDonationApp.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.DataTransferObject.Mappers
{
    public class QuestionnaireMapper : IMapperCustom<GetQuestionnaireDTO, Questionnaire>
    {
        public GetQuestionnaireDTO ToDto(Questionnaire questionnaire) => new()
        {
            QuestionnaireTitle = questionnaire.QuestionnaireTitle,
            Remark = questionnaire.Remark,
        };

        public Questionnaire FromDto(CreateQuestionnaireDTO dto, IQueryable<Question> questions) => new()
        {
            JMBG = dto.JMBG,
            ActionID = dto.ActionID,
            QuestionnaireTitle = dto.QuestionnaireTitle,
            Remark = dto.Remark,
            DateOfMaking = dto.DateOfMaking,
            ListOfQuestions = questions.Select(question => new QuestionnaireQuestion {
                QuestionID = question.QuestionID,
                Answer = false
            }).ToList()           
        };

        public Questionnaire FromDto(GetQuestionnaireDTO source)
        {
            throw new NotImplementedException();
        }
    }
}
