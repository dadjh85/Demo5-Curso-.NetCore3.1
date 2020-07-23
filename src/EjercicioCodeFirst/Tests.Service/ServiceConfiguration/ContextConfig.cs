using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestSupport.EfHelpers;

namespace Tests.Service.ServiceConfiguration
{
    public static class ContextConfig
    {
        public static async Task AddDatabaseContext<TContext, TEntity>(TContext context, List<TEntity> entities) where TContext : DbContext where TEntity : class
        {
            await context.Set<TEntity>().AddRangeAsync(entities);
            await context.SaveChangesAsync();
        }

        public static async Task InitializeDatabaseContextSedd<TContext>(TContext context) where TContext : DbContext
        {
            context.CreateEmptyViaWipe();
            await context.SaveChangesAsync();
        }
    }
}
