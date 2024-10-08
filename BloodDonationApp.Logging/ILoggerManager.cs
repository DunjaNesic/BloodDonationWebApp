﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.LoggerService
{
    public interface ILoggerManager
    {     
            void LogInformation(string message);
            void LogWarning(string message);
            void LogError(string message);
            void LogDebug(string message);
    }
}
