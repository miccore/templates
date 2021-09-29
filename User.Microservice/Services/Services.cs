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
    using User.Microservice.Data;

    using User.Microservice.Repositories.User;
    using User.Microservice.Services.User;
    using User.Microservice.Operations.User.MapperProfiles;
    using User.Microservice.Services.User.MapperProfiles;

    using User.Microservice.Repositories.Role;
    using User.Microservice.Services.Role;
    using User.Microservice.Operations.Role.MapperProfiles;
    using User.Microservice.Services.Role.MapperProfiles;

/* End Import */


namespace User.Microservice.Services {


    public class Service{

        private readonly IServiceCollection _services;
        public Service(IServiceCollection services){
            _services = services;
        }

        public Service(){}

        public void addService(){

            /** Begin Injection  */
            
                // _services.AddTransient<IStartupFilter, DBContextMigration<ApplicationDbContext>>();
                
                _services.TryAddScoped<IUserRepository, UserRepository>();
                _services.TryAddTransient<IUserService, UserService>();


                _services.TryAddScoped<IRoleRepository, RoleRepository>();
                _services.TryAddTransient<IRoleService, RoleService>();
            
            /** End Injection */
          
        }

        public IEnumerable<Profile> addProfile(){
            var profiles = new Profile[] { 
                /** Begin Adding Profiles */

                    new UserProfile(),
                    new LoginUserRequestProfile(),
                    new UserResponseProfile(),
                    new CreateUserRequestProfile(),
                    new UpdateUserRequestProfile(),

                    new RoleProfile(),
                    new RoleResponseProfile(),
                    new CreateRoleRequestProfile(),
                    new UpdateRoleRequestProfile(),

                /** End Adding Profiles */
             };

             return profiles;
        }
        

    }

}
