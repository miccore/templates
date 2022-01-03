using System;
using  Miccore.Net.webapi_template.User.Api.Entities;

namespace  Miccore.Net.webapi_template.User.Api.Operations.User.ViewModels {

    public class CreateUserViewModel {
       
       public string FirstName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
    }

}