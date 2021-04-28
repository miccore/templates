using System;
using User.Microservice.Entities;
namespace User.Microservice.Services.Role.DomainModels{

    public class RoleDomainModel : BaseEntity {
        public string Name { get; set; }
        public int Created_at {get; set;}
    }

}