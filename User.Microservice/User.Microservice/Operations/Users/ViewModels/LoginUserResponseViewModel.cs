using System;
using User.Microservice.Entities;

namespace User.Microservice.Operations.User.ViewModels {

    public class LoginUserResponseViewModel {
       
        public string Token { get; set; }
        public UserViewModel User { get; set; }
    }

}