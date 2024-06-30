using BloodDonationApp.Domain.ResponsesModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Action
{
    public class ActionNotFoundResponse : ApiNotFoundResponse
    {
        public ActionNotFoundResponse() : base($"Akcija koju trazite ne postoji u našoj bazi akcija")
        {
        }
    }
}
