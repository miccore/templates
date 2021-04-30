using Sample.Microservice.Repositories.Sample.DtoModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.Microservice.Data
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
