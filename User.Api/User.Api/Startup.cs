using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using  Miccore.Net.webapi_template.User.Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;

using  Miccore.Net.webapi_template.User.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using JWTAuthentication.Models;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace  Miccore.Net.webapi_template.User.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var host = Configuration["PMA_HOST"] ?? "localhost";
            var db = Configuration["PMA_DB"] ?? "database";
            var port = Configuration["PMA_PORT"] ?? "3306";
            var user  = Configuration["PMA_USER"] ?? "mysql_user";
            var password = Configuration["PMA_PASSWORD"] ?? "mysql_password_user";

            CorsConfiguration(services);
            services.AddDbContextPool<ApplicationDbContext>(
                    options => options.UseMySql($"server={host};port={port};database={db};user={user};password={password}" 
             ));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            //services and repository
            addServive(services);

            UserServiceCollectionExtensions.AddAutoMapper(services, GetAssemblyNamesToScanForMapperProfiles());

            #region Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "User Microservice API",
                });

                c.MapType<TimeSpan>(() => new OpenApiSchema { Type = "string", Format = "d.hh:mm:ss" });

                c.MapType<TimeSpan?>(() => new OpenApiSchema { Type = "string", Format = "d.hh:mm:ss" });

               c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
                

            });
            #endregion
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme);
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );
            services.AddControllers()
                    .AddJsonOptions(ops =>
                    {
                        ops.JsonSerializerOptions.IgnoreNullValues = true;
                        ops.JsonSerializerOptions.WriteIndented = true;
                        ops.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                        ops.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                        ops.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                    });
            services.AddMvc().AddFluentValidation();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = "http://localhost:44373/",
                            ValidAudience = "http://localhost:44373/",
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("322e9998-f1f0-494a-9b9d-aea4e0008888")),
                            ClockSkew = TimeSpan.Zero
                        };
                    });
            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
            });
        }

        //add mapper profiles
        private static IEnumerable<Profile> GetAssemblyNamesToScanForMapperProfiles()
        {
            var serv = new Service();
            var profiles = serv.addProfile();
            return profiles;
        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "User.Api");
                c.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }

        private static void UpdateDatabase(IApplicationBuilder app){
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        private static void CorsConfiguration(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(
                    "AllowSpecificOrigin",
                    builder => builder.AllowAnyOrigin() 
                        .AllowAnyHeader()
                        .WithMethods("PUT", "POST", "OPTIONS", "GET", "DELETE"));
            });
        }

        private static void addServive(IServiceCollection services)
        {
            var serv = new Service(services);
            serv.addService();
        }
    }
}
