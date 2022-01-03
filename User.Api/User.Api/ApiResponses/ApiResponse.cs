using System.Collections.Generic;

namespace Miccore.Net.webapi_template.User.Api.Operations.ApiResponses
{
    public class ApiResponse
    {
        public object Data { get; set; }

        public IEnumerable<ApiError> Errors { get; set; }
    }
}
