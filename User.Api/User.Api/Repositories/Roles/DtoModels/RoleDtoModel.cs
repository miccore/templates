using System;
using  Miccore.Net.webapi_template.User.Api.Entities;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace  Miccore.Net.webapi_template.User.Api.Repositories.Role.DtoModels {

    [Table("Roles")]
    public class RoleDtoModel : BaseEntity {
       
        [Required]
        public string Name { get; set; }
    }

}
