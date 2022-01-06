using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using  Miccore.Net.webapi_template.User.Api.Operations.ApiResponses;

namespace  Miccore.Net.webapi_template.User.Api.Operations
{
    [EnableCors("AllowSpecificOrigin")]
    [Route("api/v1/")]
    public class BaseController : ControllerBase
    {
        protected ActionResult HandleSuccessResponse(object data, HttpStatusCode status = HttpStatusCode.OK)
        {
            var response = new ApiResponse();
            response.Data = data;
            return StatusCode((int)status, response);
        }

        protected ActionResult HandleCreatedResponse(object data, HttpStatusCode status = HttpStatusCode.Created)
        {
            var response = new ApiResponse();
            response.Data = data;
            return StatusCode((int)status, response);
        }

        protected ActionResult HandleErrorResponse(HttpStatusCode httpStatus, string message)
        {
            var response = new ApiResponse()
            {
                Errors = new List<ApiError>
                {
                    new ApiError
                    {
                        Code = (int)httpStatus,
                        Message = message,
                    },
                },
            };

            return StatusCode((int)httpStatus, response);
        }

        protected ActionResult HandleDeletedResponse()
        {
            return StatusCode((int)HttpStatusCode.NoContent);
        }

        // record for Url query parameter
        public record UrlQueryParameters(int Limit = 50, int Page = 1);
    }
}
