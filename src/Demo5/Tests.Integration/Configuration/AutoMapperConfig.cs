using AutoMapper;
using Service.DtoModels.UserModel;

namespace Tests.Integration.Configuration
{
    public static class AutoMapperConfig
    {
        public static IMapper GetIMapper()
        {
            MapperConfiguration autoMapperProfilesConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMapperConfig());
            });

            return new Mapper(autoMapperProfilesConfiguration);
        }
    }
}
