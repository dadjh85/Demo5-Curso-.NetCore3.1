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
            services.AddControllers();
            services.AddHealthCheck(Configuration);
            services.AddConfigurationHealthCheckUI(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndPointWithHealthCheck();
        }
    }
}
