using Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Integration.Configuration.Database.Repository
{
    public class RolRepositoryTest
    {
        private readonly ServerFixture _serverFixture;

        public RolRepositoryTest(ServerFixture serverFixture)
        {
            _serverFixture = serverFixture;
        }

        public async Task<Rol> Add(Rol rol)
        {
            await _serverFixture.ExecuteDbContextAsync(async context =>
            {
                await context.Rol.AddAsync(rol);
                await context.SaveChangesAsync();
            });

            return rol;
        }


        public async Task<List<Rol>> AddList(List<Rol> rols)
        {
            await _serverFixture.ExecuteDbContextAsync(async context =>
            {
                await context.Rol.AddRangeAsync(rols);
                await context.SaveChangesAsync();
            });

            return rols;
        }

        public async Task<List<Rol>> GetListAsync()
        {
            List<Rol> items = null;
            await _serverFixture.ExecuteDbContextAsync(async context =>
            {
                items = await context.Rol.AsNoTracking().ToListAsync();
            });

            return items;
        }
    }
}
