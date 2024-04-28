using BloodDonationApp.Domain.ExceptionModel;
using BloodDonationApp.Domain.ExceptionModel.BaseException;
using BloodDonationApp.LoggerService;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace BloodDonationApp.API.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError => {
                appError.Run(async context => {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        logger.LogError($"Smth went wrong: {contextFeature.Error}");

                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        await context.Response.WriteAsync(new ExceptionMessage() {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }?.ToString() ?? "");
                    }
                });
            });
        }
    }
}
