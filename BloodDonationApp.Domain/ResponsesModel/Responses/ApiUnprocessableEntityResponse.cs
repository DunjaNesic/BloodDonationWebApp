using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.Responses
{
    public class ApiUnprocessableEntityResponse : ApiBaseResponse
    {
        public string ErrorMessage { get; set; }
        public ApiUnprocessableEntityResponse(string message) : base(false)
        {
            ErrorMessage = message;
        }
    }
}
