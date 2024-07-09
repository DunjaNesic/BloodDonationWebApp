using BloodDonationApp.Domain.ResponsesModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Questionnaire
{
    public class QuestionnareNotFoundResponse : ApiNotFoundResponse
    {
        public QuestionnareNotFoundResponse() : base($"Nije napravljen upitnik za tog davaoca i tu akciju")
        {
        }
    }
}
