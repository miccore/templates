using System;
using Sample.Microservice.Entities;
namespace Sample.Microservice.Services.Sample.DomainModels{

    public class SampleDomainModel : BaseEntity {
        public string Name { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }

}