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
    }
}
