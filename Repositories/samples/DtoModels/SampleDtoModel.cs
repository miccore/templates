using System;
using Sample.Microservice.Entities;

namespace Sample.Microservice.Repositories.Sample.DtoModels {

    public class SampleDtoModel : BaseEntity {
       
        public string Name { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public int CreatedAt { get; set;}
    }

}