using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace  Miccore.Net.webapi_template.User.Api.Entities
{
    public interface ILinkedResource
    {
        IDictionary<LinkedResourceType, LinkedResource> Links { get; set; }
    }
}
