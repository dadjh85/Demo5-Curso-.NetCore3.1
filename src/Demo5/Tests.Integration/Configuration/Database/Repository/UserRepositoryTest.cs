using Database.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests.Integration.Configuration.Database.Repository
{
    public class UserRepositoryTest
    {
        private readonly ServerFixture _serverFixture;

        public UserRepositoryTest(ServerFixture serverFixture)
        {
            _serverFixture = serverFixture;
        }

        public async Task<User> Add(User user)
        {
            await _serverFixture.ExecuteDbContextAsync(async context =>
            {
                await context.User.AddAsync(user);
                await context.SaveChangesAsync();
            });
            return user;
        }

        public async Task<List<User>> AddList(List<User> items)
        {
            await _serverFixture.ExecuteDbContextAsync(async context =>
            {
                await context.User.AddRangeAsync(items);
                await context.SaveChangesAsync();
            });
            return items;
        }

        public async Task<List<User>> GetListAsync()
        {
            List<User> items = null;

            await _serverFixture.ExecuteDbContextAsync(async context =>
            {
                items = await context.User.AsNoTracking().ToListAsync();
            });

            return items;
        }
    }
}
