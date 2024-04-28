using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.LoggerService
{
    public class ConsoleLogger : ILoggerManager
    {
        public void LogInformation(string message)
        {
            Console.BackgroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Information: {message}");
            Console.ResetColor();
        }

        public void LogWarning(string message)
        {
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Warning: {message}");
            Console.ResetColor();
        }

        public void LogError(string message)
        {
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine($"Error: {message}");
            Console.ResetColor();
        }

        public void LogDebug(string message)
        {
            Console.BackgroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine($"Debug: {message}");
            Console.ResetColor();
        }


        //private static ILogger logger = LogManager.GetCurrentClassLogger();
        //public ConsoleLogger()
        //{
        //}
        //public void LogDebug(string message) => logger.Debug(message);
        //public void LogError(string message) => logger.Error(message);
        //public void LogInformation(string message) => logger.Info(message);
        //public void LogWarning(string message) => logger.Warn(message);

    }
}
