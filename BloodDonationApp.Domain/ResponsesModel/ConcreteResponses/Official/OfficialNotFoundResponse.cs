using BloodDonationApp.Domain.ResponsesModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Official
{
    public sealed class OfficialNotFoundResponse : ApiNotFoundResponse
    {
        public OfficialNotFoundResponse() : base("Trazeni sluzbenik ne postoji u bazi sluzbenika")
        {
        }
    }
}
