using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DtoModels.UserModel
{
    public class DtoUserGet
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int YearsOld { get; set; }
        public string Email { get; set; }

        public DtoRolGet RolNavigation { get; set; }

    }
}
