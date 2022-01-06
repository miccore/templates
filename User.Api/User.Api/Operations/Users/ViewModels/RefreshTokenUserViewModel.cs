using System;
using  Miccore.Net.webapi_template.User.Api.Entities;
using  Miccore.Net.webapi_template.User.Api.Operations.Role.ViewModels;

namespace  Miccore.Net.webapi_template.User.Api.Operations.User.ViewModels {

    public class RefreshTokenUserViewModel : BaseEntity {
       
       
        public string RefreshToken { get; set; }
    }

}