using System;
using User.Microservice.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace User.Microservice.Repositories.Role.DtoModels {

    [Table("Roles")]
    public class RoleDtoModel : BaseEntity {
       
        [Required]
        public string Name { get; set; }
    }

}
