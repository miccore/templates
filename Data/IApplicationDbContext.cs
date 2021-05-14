/** Begin Import */

    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using User.Microservice.Repositories.User.DtoModels;
    using User.Microservice.Repositories.Role.DtoModels;

/* End Import */

namespace User.Microservice.Data
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
