using AutoMapper;
using Database.Model;

namespace Service.DtoModels.UserModel
{
    public class UserMapperConfig : Profile
    {
        public UserMapperConfig()
        {
            CreateMap<User, DtoUserGet>();
            CreateMap<DtoUserAdd, User>();
            CreateMap<DtoUserUpdate, User>();
            CreateMap<Rol, DtoRolGet>();
        }
    }
}
