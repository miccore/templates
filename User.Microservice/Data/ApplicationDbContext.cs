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
    public class ApplicationDbContext : DbContext,IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /** Begin DBContext Adding */
            
            DbSet<UserDtoModel> IApplicationDbContext.Users { get; set; }
            DbSet<RoleDtoModel> IApplicationDbContext.Roles { get; set; }

        /** End DBContext Adding */

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
