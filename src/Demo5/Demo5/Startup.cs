using System;
using System.Collections.Generic;
using System.Reflection;
using Database;
using Demo5.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Service.DtoModels.UserModel;

[assembly: ApiController]
[assembly: ApiConventionType(typeof(DefaultApiConventions))]
namespace Demo5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DemoContext>(c => c.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddAutoMapper(typeof(UserMapperConfig).GetTypeInfo().Assembly);
            services.AddScrutor();
            services.AddControllersWithDefaultCache(Configuration);
            services.AddHealthCheck(Configuration);
            services.AddConfigurationHealthCheckUI(Configuration);
            services.AddResponseCaching();
            services.AddSwagger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger(env);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseResponseCaching();

            app.UseEndPointWithHealthCheck();
        }
    }
}
