
using Database.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.UserRepository
{
    public interface IUserRepository
    {
        IQueryable<User> Get(int id);

        IQueryable<User> GetList();

        Task<User> FindAsync(int id);

        Task<User> AddAsync(User item);

        Task<int?> UpdateAsync(User item);

        Task<int?> Delete(User item);
    }
}
