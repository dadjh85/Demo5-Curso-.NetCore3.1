using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

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
    }
}
