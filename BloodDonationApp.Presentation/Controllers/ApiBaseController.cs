using BloodDonationApp.Domain.ExceptionModel;
using BloodDonationApp.Domain.ResponsesModel.BaseApiResponse;
using BloodDonationApp.Domain.ResponsesModel.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodDonationApp.Presentation.Controllers
{
    public class ApiBaseController : ControllerBase
    {

        [ApiExplorerSettings(IgnoreApi = true)]
        public ActionResult ProcessError(ApiBaseResponse baseResponse)
        {
            return baseResponse switch
            {
                ApiNotFoundResponse => NotFound(new ExceptionMessage
                {
                    Message = ((ApiNotFoundResponse)baseResponse).ErrorMessage,
                    StatusCode = StatusCodes.Status404NotFound
                }),
                ApiBadRequestResponse => BadRequest(new ExceptionMessage
                {
                    Message = ((ApiBadRequestResponse)baseResponse).ErrorMessage,
                    StatusCode = StatusCodes.Status400BadRequest
                }),
                ApiUnauthorizedResponse => Unauthorized(new ExceptionMessage
                {
                    Message = ((ApiUnauthorizedResponse)baseResponse).ErrorMessage,
                    StatusCode = StatusCodes.Status401Unauthorized
                }),
                ApiForbiddenResponse => StatusCode(StatusCodes.Status403Forbidden, new ExceptionMessage
                {
                    Message = ((ApiForbiddenResponse)baseResponse).ErrorMessage,
                    StatusCode = StatusCodes.Status403Forbidden
                }),
                ApiUnavailableForLegalReasonsResponse => StatusCode(StatusCodes.Status451UnavailableForLegalReasons, new ExceptionMessage
                {
                    Message = ((ApiUnavailableForLegalReasonsResponse)baseResponse).ErrorMessage,
                    StatusCode = StatusCodes.Status451UnavailableForLegalReasons
                }),
                ApiUnprocessableEntityResponse => StatusCode(StatusCodes.Status422UnprocessableEntity, new ExceptionMessage
                {
                    Message = ((ApiUnprocessableEntityResponse)baseResponse).ErrorMessage,
                    StatusCode = StatusCodes.Status422UnprocessableEntity
                }),
                _ => throw new NotImplementedException()
            };
        }
    }
}
