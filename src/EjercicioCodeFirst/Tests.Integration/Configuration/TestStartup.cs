using Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Service.DtoModels.UserModel;
using System.Reflection;
using AutoMapper;
using EjercicioCodeFirst.Infrastructure;

namespace Tests.Integration.Configuration
{
    public class TestStartup
    {
        public TestStartup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScrutor();
            services.AddDbContext<DemoContext>(c => c.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddControllers();
            services.AddAutoMapper(typeof(UserAutoMapperConfig).GetTypeInfo().Assembly);
            services.AddHttpContextAccessor();
            services.AddMvcCore().AddApplicationPart(Assembly.Load(new AssemblyName(nameof(EjercicioCodeFirst))));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
