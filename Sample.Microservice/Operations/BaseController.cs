using System.Collections.Generic;
using System.Net;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Sample.Microservice.Operations.ApiResponses;

namespace Sample.Microservice.Operations
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
    }
}
