using Database;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.RolRepository
{
    public class RolRepository : IRolRepository
    {
        private readonly DemoContext _context;

        public RolRepository(DemoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IQueryable<Rol> Get(int id)
            => _context.Rol.Where(e => e.Id == id).AsNoTracking();

        public IQueryable<Rol> GetList()
            => _context.Rol.AsNoTracking();

        public async Task<Rol> FindAsync(int id)
            => await _context.Rol.FindAsync(id);

        public async Task<Rol> AddAsync(Rol item)
        {
            if (item != null)
            {
                await _context.Rol.AddAsync(item);
                await _context.SaveChangesAsync();

                return item;
            }

            return null;
        }

        public async Task<int?> UpdateAsync(Rol item)
        {
            if (item != null)
            {
                _context.Rol.Update(item);
                return await _context.SaveChangesAsync();
            }

            return null;
        }

        public async Task<int?> Delete(Rol item)
        {
            if (item != null)
            {
                _context.Rol.Remove(item);
                return await _context.SaveChangesAsync();
            }

            return null;
        }
    }
}
