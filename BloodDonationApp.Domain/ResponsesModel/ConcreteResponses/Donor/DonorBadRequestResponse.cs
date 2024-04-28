using BloodDonationApp.Domain.ResponsesModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Donor
{
    public sealed class DonorBadRequestResponse : ApiBadRequestResponse
    {
        public DonorBadRequestResponse() : base("Ne mozemo da obradimo vaš zahtev i pronadjemo davaoca sa tim JMBG-om")
        {
        }
    }
}
