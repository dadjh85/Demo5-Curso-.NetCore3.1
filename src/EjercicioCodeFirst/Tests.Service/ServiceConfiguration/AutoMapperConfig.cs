using AutoMapper;
using Service.DtoModels.UserModel;

namespace Tests.Service.ServiceConfiguration
{
    public static class AutoMapperConfig
    {
        public static IMapper GetIMapper()
        {
            MapperConfiguration mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserAutoMapperConfig());
            });

            return new Mapper(mapperConfiguration);
        }
    }
}
