using Database;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        #region Properties

        private readonly DemoContext _context;

        #endregion

        public UserRepository(DemoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        #region Implementation IUserRepository

        public IQueryable<User> Get(int id)
              => _context.User.Where(e => e.Id == id).AsNoTracking();

        public IQueryable<User> GetList()
            => _context.User.AsNoTracking();


        public async Task<User> FindAsync(int id)
            => await _context.User.FindAsync(id);

        public async Task<User> AddAsync(User item)
        {
            if (item != null)
            {
                await _context.User.AddAsync(item);
                await _context.SaveChangesAsync();

                return item;
            }

            return null;
        }

        public async Task UpdateAsync(User item)
        {
            if (item != null)
            {
                _context.User.Update(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task Delete(User item)
        {
            if (item != null)
            {
                _context.User.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        #endregion

    }
}
