using Demo5.Infrastructure.MapperOptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using HealthChecksStatus = Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus;

namespace Demo5.Infrastructure
{
    public static class IServiceCollectionExtension
    {
        public static IServiceCollection AddScrutor(this IServiceCollection services)
        {
            services.Scan(s => s.FromAssemblies(Assembly.Load("Service"), Assembly.Load("Repository"))
                                .AddClasses(c => c.Where(e => e.Name.EndsWith("Service") || e.Name.EndsWith("Repository")))
                                .AsImplementedInterfaces()
                                .WithScopedLifetime());
            return services;
        }

        public static IServiceCollection AddConfigurationHealthCheckUI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecksUI()
                    .AddSqlServerStorage(configuration.GetConnectionString("Default"));
            return services;
        }

        public static IServiceCollection AddControllersWithDefaultCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(option => option.CacheProfiles.Add("DefaultCache", new CacheProfile()
            {
                Duration = configuration.GetValue<int>("Cache:Time"),
                Location = ResponseCacheLocation.Any
            }));

            return services;
        }

        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<HealthMapperOptions>(configuration.GetSection("HealthConfig"));
            HealthMapperOptions healthConfig = new HealthMapperOptions();
            configuration.GetSection("HealthConfig").Bind(healthConfig);
            services.AddHealthChecks()
                    //Validation Disk
                    .AddDiskStorageHealthCheck(opt =>
                    {
                        opt.AddDrive(driveName: healthConfig.DiskConfig.Name,
                                     minimumFreeMegabytes: healthConfig.DiskConfig.MinimunSize);
                    })
                    //Validation Memory
                    .AddPrivateMemoryHealthCheck(maximumMemoryBytes: healthConfig.MemoryConfig.MaximunMemorySize,
                                                 failureStatus: HealthChecksStatus.Degraded,
                                                 name: healthConfig.MemoryConfig.Name)
                    //Validation de SQL Server
                    .AddSqlServer(connectionString: configuration.GetConnectionString("Default"),
                                  healthQuery: "SELECT 1",
                                  name: healthConfig.SqlServer.Name,
                                  failureStatus: HealthChecksStatus.Degraded);
            return services;
        }
    }
}
