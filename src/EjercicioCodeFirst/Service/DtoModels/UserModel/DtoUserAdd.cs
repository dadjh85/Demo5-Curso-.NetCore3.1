using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Service.DtoModels.UserModel
{
    public class DtoUserAdd
    {

        [MaxLength(50), Required]
        public string UserName { get; set; }
        public int YearsOld { get; set; }

        [MaxLength(50), Required, EmailAddress]
        public string Email { get; set; }

        [MaxLength(200), Required]
        public string Password { get; set; }

        public bool Active { get; set; }

        public int IdRol { get; set; }

    }
}
