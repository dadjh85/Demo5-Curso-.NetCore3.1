using AutoMapper;
using Database;
using Demo5.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service.DtoModels.UserModel;
using System.Reflection;

//[assembly: ApiController]
//[assembly: ApiConventionType(typeof(DefaultApiConventions))]
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
            services.AddDbContext<DemoContext>(c => c.UseSqlServer(Configuration.GetConnectionString("Default")));
            services.AddAutoMapper(typeof(UserMapperConfig).GetTypeInfo().Assembly);
            services.AddScrutor();
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddMvcCore().AddApplicationPart(Assembly.Load(new AssemblyName(nameof(Demo5))));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
