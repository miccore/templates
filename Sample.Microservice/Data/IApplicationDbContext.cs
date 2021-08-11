/** Begin Import */

    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Sample.Microservice.Repositories.Sample.DtoModels;

/* End Import */

namespace Sample.Microservice.Data
{
    public interface IApplicationDbContext
    {

        /** Begin Interface DBContext Adding */

            DbSet<SampleDtoModel> Samples{ get; set; }

        /** End Interface DBContext Adding */
        Task<int> SaveChanges();
    }
}
