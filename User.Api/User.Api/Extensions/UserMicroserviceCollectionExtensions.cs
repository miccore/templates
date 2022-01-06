using System.Collections.Generic;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class UserServiceCollectionExtensions
    {
            public static IServiceCollection AddAutoMapper(this IServiceCollection services, IEnumerable<Profile> assemblyNamesToScan)
        {
            services.TryAddSingleton(GenerateMapperConfiguration(assemblyNamesToScan));
            return services;
        }

        private static IMapper GenerateMapperConfiguration(IEnumerable<Profile> assemblyNamesToScan)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfiles(assemblyNamesToScan);
            });
            return config.CreateMapper();
        }


        private static void AddSecurityDefinition(SwaggerGenOptions options) =>
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Description = "Please insert JWT with Bearer into field",
            });

        private static void AddSecurityRequirement(SwaggerGenOptions options) =>
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" },
                    },
                    new[] { "readAccess", "writeAccess" }
                },
            });
    }
}
