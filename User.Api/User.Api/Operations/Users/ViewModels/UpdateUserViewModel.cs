using System;
using Miccore.Net.webapi_template.User.Api.Entities;

namespace Miccore.Net.webapi_template.User.Api.Operations.User.ViewModels {

    public class UpdateUserViewModel : BaseEntity {
       
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

}