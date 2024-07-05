using BloodDonationApp.Domain.ResponsesModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Action
{
    public sealed class ActionBadRequestResponse : ApiBadRequestResponse
    {
        public ActionBadRequestResponse(string message) : base(message)
        {
        }
    }
}
