using System;
using Miccore.Net.webapi_template.User.Api.Entities;
using Miccore.Net.webapi_template.User.Api.Services.Role.DomainModels;
namespace Miccore.Net.webapi_template.User.Api.Services.User.DomainModels{

    public class UserDomainModel : BaseEntity {
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string RefreshToken { get; set; }
        public string Address { get; set; }
        public string? Token { get; set;}
        public int RoleId { get; set;}
        public RoleDomainModel Role {get; set;}
    }

}
