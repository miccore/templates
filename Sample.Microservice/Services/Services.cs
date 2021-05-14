/** 
    This file is the file that make importations, injections dependancies, and profiles adding

 */

/** Begin Import */

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.DependencyInjection.Extensions;
    using AutoMapper;

    using Sample.Microservice.Repositories.Sample;
    using Sample.Microservice.Services.Sample;
    using Sample.Microservice.Operations.Sample.MapperProfiles;
    using Sample.Microservice.Services.Sample.MapperProfiles;

/* End Import */


namespace Sample.Microservice.Services {


    public class Service{

        private readonly IServiceCollection _services;
        public Service(IServiceCollection services){
            _services = services;
           
        }

        public Service(){}

        public void addService(){
             /** Begin Injection  */
            
                _services.TryAddScoped<ISampleRepository, SampleRepository>();
                _services.TryAddTransient<ISampleService, SampleService>();
            
            /** End Injection */
          
        }

        public IEnumerable<Profile> addProfile(){
            var profiles = new Profile[] { 

                /** Begin Adding Profiles */

                    new SampleProfile(),
                    new SampleResponseProfile(),
                    new CreateSampleRequestProfile(),
                    new UpdateSampleRequestProfile(),

                /** End Adding Profiles */
                
             };

             return profiles;
        }
        

    }

}
