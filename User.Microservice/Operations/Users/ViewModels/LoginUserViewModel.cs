using System;
using User.Microservice.Entities;

namespace User.Microservice.Operations.User.ViewModels {

    public class LoginUserViewModel {
       
        public string Phone { get; set; }
        public string Password { get; set; }
    }

}