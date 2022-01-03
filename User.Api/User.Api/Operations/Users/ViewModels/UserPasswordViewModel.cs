using System;
using System.Collections.Generic;
using Miccore.Net.webapi_template.User.Api.Entities;

namespace Miccore.Net.webapi_template.User.Api.Operations.User.ViewModels {

    public class UserPasswordViewModel : BaseEntity {
       
        public string odlpassword { get; set; }
        public string newpassword { get; set; }
    }

}