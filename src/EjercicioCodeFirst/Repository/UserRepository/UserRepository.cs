using Database;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;

namespace Repository.UserRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly DemoContext _demoContext;
        
        public UserRepository(DemoContext demoContext)
        {
            _demoContext = demoContext ?? throw new ArgumentNullException(nameof(demoContext));
        }

        public IQueryable<User> Get(int id)
        {
            return _demoContext.User.Where(e => e.Id == id).AsNoTracking();
        }

        public async Task<User> FindAsync(int id)
        {
            return await _demoContext.User.FindAsync(id);
        }

        public IQueryable<User> GetList()
        {
            return _demoContext.User.AsNoTracking();
        }

        public async Task<User> AddAsync(User item)
        {
            if(item != null)
            {
                await _demoContext.User.AddAsync(item);
                await _demoContext.SaveChangesAsync();
            }

            return item;
        }

        public async Task<int?> UpdateAsync(User item)
        {
            if(item != null)
            {
                _demoContext.User.Update(item);
                return await _demoContext.SaveChangesAsync();
            }

            return null;
        }

        public async Task<int?> Delete(User item)
        {
            if(item != null)
            {
                _demoContext.User.Remove(item);
                return await _demoContext.SaveChangesAsync();
            }

            return null;
        }

    }
}
