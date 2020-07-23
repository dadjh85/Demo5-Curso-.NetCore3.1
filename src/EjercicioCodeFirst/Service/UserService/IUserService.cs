using Service.DtoModels.UserModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.UserService
{
    public interface IUserService
    {
        Task<DtoUserGet> Get(int id);

        Task<List<DtoUserGet>> GetList();

        Task<int?> AddAsync(DtoUserAdd item);

        Task<int?> UpdateAsync(DtoUserUpdate item);

        Task<int?> Delete(int id);
    }
}
