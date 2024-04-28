using BloodDonationApp.Domain.ResponsesModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Donor
{
    public sealed class DonorNotFoundResponse : ApiNotFoundResponse
    {
        public DonorNotFoundResponse() : base($"Davalac sa tim JMBG-om ne postoji u našoj bazi dobrovoljnih davaoca krvi")
        {
        }
    }
}
