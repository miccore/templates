using System;
using Miccore.Net.webapi_template.Sample.Api.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Miccore.Net.webapi_template.Sample.Api.Repositories.Sample.DtoModels {
    
    [Table("Samples")]
    public class SampleDtoModel : BaseEntity {
       
        public string Name { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }

}
