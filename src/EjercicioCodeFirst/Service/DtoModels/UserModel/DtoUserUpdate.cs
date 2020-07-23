using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Service.DtoModels.UserModel
{
    public class DtoUserUpdate
    {
        public int Id { get; set; }

        [MaxLength(200), Required]
        public string Password { get; set; }

        [MaxLength(50), Required, EmailAddress]
        public string Email { get; set; }
    }
}
