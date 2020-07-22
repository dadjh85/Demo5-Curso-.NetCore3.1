using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Database.Model
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(50), Required]
        public string UserName { get; set; }
        public int YearsOld { get; set; }

        [MaxLength(50), Required]
        public string Email { get; set; }

        [MaxLength(200), Required]
        public string Password { get; set; }

        public bool Active { get; set; }

        #region Properties Relations

        public int IdRol { get; set; }

        #endregion

        #region Navigations

        public Rol RolNavigation { get; set; }

        #endregion

    }
}
