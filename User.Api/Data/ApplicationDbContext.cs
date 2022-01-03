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
