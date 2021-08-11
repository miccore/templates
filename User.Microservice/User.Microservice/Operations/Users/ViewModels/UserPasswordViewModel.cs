using System;
using System.Collections.Generic;
using User.Microservice.Entities;

namespace User.Microservice.Operations.User.ViewModels {

    public class UserPasswordViewModel : BaseEntity {
       
        public string odlpassword { get; set; }
        public string newpassword { get; set; }
    }

}