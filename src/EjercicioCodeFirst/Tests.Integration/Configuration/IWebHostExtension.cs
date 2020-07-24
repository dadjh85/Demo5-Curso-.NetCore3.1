using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Tests.Integration.Configuration
{
    public static class IWebHostExtension
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {
            using(var scope = webHost.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;

                TContext context = services.GetService<TContext>();

                try
                {
                    context.Database.Migrate();
                    seeder(context, services);
                }
                catch(Exception ex)
                {
                    
                }

                return webHost;
            }
        }
    }
}
