using Service.DtoModels.UserModel;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service.UserService
{
    public interface IUserService
    {
        Task<DtoUserGet> Get(int id);

        Task<List<DtoUserGet>> GetList();

        Task<int> Add(DtoUserAdd item);

        Task Update(DtoUserUpdate item);

        Task Delete(int id);
    }
}
