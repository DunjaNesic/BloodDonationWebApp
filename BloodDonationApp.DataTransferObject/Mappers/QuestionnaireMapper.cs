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
        public QuestionnaireQuestionDTO ToDto(QuestionnaireQuestion question) => new()
        {
            QuestionID = question.QuestionID,
            Answer = question.Answer
        };
        public GetQuestionnaireDTO ToDto(Questionnaire questionnaire) => new()
        {
            QuestionnaireTitle = questionnaire.QuestionnaireTitle,
            Remark = questionnaire.Remark,
            AnsweredQuestions = questionnaire.ListOfQuestions?.Select(q => this.ToDto(q)).ToList()
        };

        public Questionnaire FromDto(CreateQuestionnaireDTO dto, IEnumerable<Question> questions)
        {
            var questionList = questions.ToList();

            while (dto.Answers.Count < questionList.Count)
            {
                dto.Answers.Add(false);
            }

            return new Questionnaire
            {
                QuestionnaireTitle = dto.QuestionnaireTitle,
                Remark = dto.Remark,
                DateOfMaking = dto.DateOfMaking,
                ListOfQuestions = questionList.Select((question, index) => new QuestionnaireQuestion
                {
                    QuestionID = question.QuestionID,
                    Answer = dto.Answers[index],
                    RowVersion = new byte[0]
                }).ToList()
            };
        }

        public Questionnaire FromDto(UpdateQuestionnaireDTO dto, IEnumerable<Question> questions) {

            var questionList = questions.ToList();

            while (dto.Answers.Count < questionList.Count)
            {
                dto.Answers.Add(false);
            }         

            return new Questionnaire() { 
                Remark = dto.Remark,
                Approved = dto.Approved,
                RowVersion = dto.RowVersion,
                ListOfQuestions = questionList.Select((question, index) => new QuestionnaireQuestion
                {
                    QuestionID = question.QuestionID,
                    Answer = dto.Answers[index],
                    RowVersion = new byte[0]
                }).ToList()
            };
    }
        public Questionnaire FromDto(GetQuestionnaireDTO source)
        {
            throw new NotImplementedException();
        }
    }
}
