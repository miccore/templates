using System;
using Sample.Microservice.Entities;

namespace Sample.Microservice.Operations.Sample.ViewModels {

    public class CreateSampleViewModel {
       
        public string Name { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
    }

}