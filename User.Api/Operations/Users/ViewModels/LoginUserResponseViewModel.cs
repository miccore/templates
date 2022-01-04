using System;
using Miccore.Net.webapi_template.User.Api.Entities;

namespace Miccore.Net.webapi_template.User.Api.Operations.User.ViewModels {

    public class LoginUserResponseViewModel {
       
        public string Token { get; set; }
        public UserViewModel User { get; set; }
    }

}