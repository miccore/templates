/** Begin Import */

    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using  Miccore.Net.webapi_template.User.Api.Repositories.User.DtoModels;
    using  Miccore.Net.webapi_template.User.Api.Repositories.Role.DtoModels;

/* End Import */

namespace  Miccore.Net.webapi_template.User.Api.Data
{
    public interface IApplicationDbContext
    {
        /** Begin Interface DBContext Adding */

            DbSet<UserDtoModel> Users{ get; set; }
            DbSet<RoleDtoModel> Roles{ get; set; }
        
        /** End Interface DBContext Adding */
        Task<int> SaveChanges();
    }
}
