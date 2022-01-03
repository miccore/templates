/** Begin Import */

    using Miccore.Net.webapi_template.Sample.Api.Repositories.Sample.DtoModels;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

/* End Import */

namespace Miccore.Net.webapi_template.Sample.Api.Data
{
    public class ApplicationDbContext : DbContext,IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /** Begin DBContext Adding */

            DbSet<SampleDtoModel> IApplicationDbContext.Samples { get; set; }
        
        /** End DBContext Adding */

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }
    }
}
