using AutoMapper;
using Database.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DtoModels.UserModel
{
    public class UserAutoMapperConfig : Profile
    {
        public UserAutoMapperConfig()
        {
            CreateMap<User, DtoUserGet>();
            CreateMap<Rol, DtoRolGet>();
            CreateMap<DtoUserAdd, User>();
            CreateMap<DtoUserUpdate, User>();
        }
    }
}
