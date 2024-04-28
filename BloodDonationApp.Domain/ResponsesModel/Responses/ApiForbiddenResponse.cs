using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.Responses
{
    public abstract class ApiForbiddenResponse : ApiBaseResponse
    {
        public string ErrorMessage { get; set; }
        protected ApiForbiddenResponse(string message) : base(false)
        {
            ErrorMessage = message;
        }

    }
}
