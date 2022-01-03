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
    using Microsoft.AspNetCore.Hosting;
    using Miccore.Net.webapi_template.Sample.Api.Data;

    using Miccore.Net.webapi_template.Sample.Api.Repositories.Sample;
    using Miccore.Net.webapi_template.Sample.Api.Services.Sample;
    using Miccore.Net.webapi_template.Sample.Api.Operations.Sample.MapperProfiles;
    using Miccore.Net.webapi_template.Sample.Api.Services.Sample.MapperProfiles;

/* End Import */


namespace Miccore.Net.webapi_template.Sample.Api.Services {


    public class Service{

        private readonly IServiceCollection _services;
        public Service(IServiceCollection services){
            _services = services;
           
        }

        public Service(){}

        public void addService(){
             /** Begin Injection  */
            
                // _services.AddTransient<IStartupFilter, DBContextMigration<ApplicationDbContext>>();
                
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
