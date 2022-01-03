/** Begin Import */

    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Miccore.Net.webapi_template.Sample.Api.Repositories.Sample.DtoModels;

/* End Import */

namespace Miccore.Net.webapi_template.Sample.Api.Data
{
    public interface IApplicationDbContext
    {

        /** Begin Interface DBContext Adding */

            DbSet<SampleDtoModel> Samples{ get; set; }

        /** End Interface DBContext Adding */
        Task<int> SaveChanges();
    }
}
