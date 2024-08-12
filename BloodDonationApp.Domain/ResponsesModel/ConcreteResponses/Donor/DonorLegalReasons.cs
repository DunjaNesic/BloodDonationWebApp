using BloodDonationApp.Domain.ResponsesModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.ConcreteResponses.Donor
{
    public class DonorLegalReasons : ApiUnavailableForLegalReasonsResponse
    {
        public DonorLegalReasons(string message) : base(message)
        {
        }
    }
}
