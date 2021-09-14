
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace User.Microservice
{
    public class DBContextMigration<TContext> : IStartupFilter where TContext : DbContext
    {
        public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
        {
            return builder  => {
                    using (var scope = builder.ApplicationServices.CreateScope()){
                    {

                        foreach (var context in scope.ServiceProvider.GetServices<TContext>())
                        {
                            context.Database.SetCommandTimeout(1000);
                            context.Database.Migrate();
                            context.Database.SetCommandTimeout(1000);
                        }
                    }
                }
                next(builder);
            };
        }

    }
}