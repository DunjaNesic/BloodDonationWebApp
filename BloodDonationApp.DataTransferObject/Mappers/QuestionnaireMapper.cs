using BloodDonationApp.DataTransferObject;
using BloodDonationApp.DataTransferObject.Questionnaires;
using BloodDonationApp.Domain.DomainModel;
using QRCoder;
using System.IO;
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
            AnsweredQuestions = questionnaire.ListOfQuestions
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
                JMBG = dto.JMBG,
                ActionID = dto.ActionID,
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

            string? qrCodeImage = null;
            if (dto.Approved)
            {
                using (var qrGenerator = new QRCodeGenerator())
                using (var qrCodeData = qrGenerator.CreateQrCode("Approved", QRCodeGenerator.ECCLevel.Q))
                using (var qrCode = new QRCode(qrCodeData))
                using (var qrCodeImageBitmap = qrCode.GetGraphic(20))
                using (var stream = new MemoryStream())
                {
                    qrCodeImageBitmap.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    qrCodeImage = Convert.ToBase64String(stream.ToArray());
                }
            }

            return new Questionnaire() { 
                Remark = dto.Remark,
                Approved = dto.Approved,
                QRCode = qrCodeImage,
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
