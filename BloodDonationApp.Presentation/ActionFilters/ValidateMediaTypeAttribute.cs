using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Net.Http.Headers;

namespace BloodDonationApp.Presentation.ActionFilters
{
    public class ValidateMediaTypeAttribute : IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext context)
        {
            const string acceptHeaderName = "Accept";

            var acceptHeaderPresent = context.HttpContext.Request.Headers.ContainsKey(acceptHeaderName);
            if (!acceptHeaderPresent)
            {
                context.Result = new BadRequestObjectResult("Accept header is missing.");
                return;
            }

            var mediaTypeHeaderValue = context.HttpContext.Request.Headers[acceptHeaderName].FirstOrDefault();
            if (string.IsNullOrEmpty(mediaTypeHeaderValue))
            {
                context.Result = new BadRequestObjectResult("Media type not present. Please add Accept header with the required media type.");
                return;
            }

            var mediaTypes = mediaTypeHeaderValue.Split(',').Select(mt => mt.Trim()).ToList();

            bool isValidMediaType = false;
            foreach (var mediaType in mediaTypes)
            {
                if (MediaTypeHeaderValue.TryParse(mediaType, out MediaTypeHeaderValue? outMediaType))
                {
                    context.HttpContext.Items.Add("AcceptHeaderMediaType", outMediaType);
                    isValidMediaType = true;
                    break;
                }
            }

            if (!isValidMediaType)
            {
                context.Result = new BadRequestObjectResult("Invalid media type. Please add a valid Accept header with the required media type.");
                return;
            }
        }
            public void OnActionExecuted(ActionExecutedContext context) { }
    }
}
