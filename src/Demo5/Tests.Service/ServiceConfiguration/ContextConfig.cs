
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestSupport.EfHelpers;

namespace Tests.Service.ServiceConfiguration
{
    public static class ContextConfig
    {
        public static async Task AddDatabaseContext<TContext, TEntity>(TContext seedContext, List<TEntity> entidades)
            where TContext : DbContext
            where TEntity : class
        {
            await seedContext.Set<TEntity>().AddRangeAsync(entidades);
            await seedContext.SaveChangesAsync();
        }

        public static async Task InitializeDatabaseContextSeed<TContext>(TContext seedContext)
            where TContext : DbContext
        {
            seedContext.CreateEmptyViaWipe();
            await seedContext.SaveChangesAsync();
        }
    }
}
