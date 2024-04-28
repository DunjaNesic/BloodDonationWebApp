﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Domain.ResponsesModel.BaseApiResponse
{
    public abstract class ApiBaseResponse
    {
        public bool Success { get; set; }
        public ApiBaseResponse(bool success) => Success = success;
    }
}
