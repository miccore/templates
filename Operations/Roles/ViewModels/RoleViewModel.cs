using System;
using User.Microservice.Entities;

namespace User.Microservice.Operations.Role.ViewModels {

    public class RoleViewModel : BaseEntity {
       
       
        public string Name { get; set; }
        public int Created_at {get; set;}
    }

}