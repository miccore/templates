using System;
using User.Microservice.Entities;
using User.Microservice.Operations.Role.ViewModels;

namespace User.Microservice.Operations.User.ViewModels {

    public class RefreshTokenUserViewModel : BaseEntity {
       
       
        public string RefreshToken { get; set; }
    }

}