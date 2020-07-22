
namespace Service.DtoModels.UserModel
{
    public class DtoUserGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int IdRol { get; set; }

        public DtoRolGet RolNavigation { get; set; }
    }
}
