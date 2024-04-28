using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.Responses
{
    public sealed class ApiOkResponse<T> : ApiBaseResponse
    {
        public T Result { get; set; }
        public ApiOkResponse(T result) : base(true)
        {
            Result = result;
        }
    }
}
