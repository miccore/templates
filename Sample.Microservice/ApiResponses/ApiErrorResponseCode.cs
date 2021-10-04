using System.ComponentModel;

namespace User.Microservice.Operations.ApiResponses.Enumerators
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1602:Enumeration items should be documented", Justification = "Enum names are self documenting.")]
    public enum ApiErrorResponseCode
    {
        [Description("Not Found")]
        NOT_FOUND,
        [Description("Already exist")]
        ALREADY_EXIST,
        [Description("Bad Request.")]
        BAD_REQUEST,
        [Description("Unauthorized.")]
        UNAUTHORIZED,
        [Description("Internal Server Error.")]
        INTERNAL_SERVER_ERROR,
        [Description("Exception.")]
        EXCEPTION,
    }
}
