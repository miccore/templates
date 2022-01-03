using System;
using Miccore.Net.webapi_template.Sample.Api.Entities;
namespace Miccore.Net.webapi_template.Sample.Api.Services.Sample.DomainModels{

    public class SampleDomainModel : BaseEntity {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }

}