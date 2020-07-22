using System;
using System.Collections.Generic;
using System.Text;

namespace Service.DtoModels.UserModel
{
    public class DtoUserAdd
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int IdRol { get; set; }
    }
}
