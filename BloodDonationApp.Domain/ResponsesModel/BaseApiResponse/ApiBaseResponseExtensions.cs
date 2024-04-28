using BloodDonationApp.Domain.ResponsesModel.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.BaseApiResponse
{
    public static class ApiBaseResponseExtensions
    {
        public static T GetResult<T>(this ApiBaseResponse apiBaseResponse) =>
        ((ApiOkResponse<T>)apiBaseResponse).Result;
    }
}
