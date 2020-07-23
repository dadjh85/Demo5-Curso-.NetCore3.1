using Database.Model;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.RolRepository
{
    public interface IRolRepository
    {
        IQueryable<Rol> Get(int id);

        IQueryable<Rol> GetList();

        Task<Rol> FindAsync(int id);

        Task<Rol> AddAsync(Rol item);

        Task<int?> UpdateAsync(Rol item);

        Task<int?> Delete(Rol item);
    }
}
