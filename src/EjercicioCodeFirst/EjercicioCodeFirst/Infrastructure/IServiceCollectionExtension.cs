using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace EjercicioCodeFirst.Infrastructure
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

        public static IServiceCollection AddControllersWithProfileCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddControllers(option => option.CacheProfiles.Add("DefaultCache", new CacheProfile()
            {
                Duration = configuration.GetValue<int>("Cache:Time"),
                Location = ResponseCacheLocation.Any
            }));

            return services;
        }
    }
}
