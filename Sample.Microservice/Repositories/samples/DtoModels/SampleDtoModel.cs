using System;
using Sample.Microservice.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace Sample.Microservice.Repositories.Sample.DtoModels {
    [Table("Samples")]
    public class SampleDtoModel : BaseEntity {
       
        public string Name { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }

}
