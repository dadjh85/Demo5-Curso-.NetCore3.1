
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Database.Model
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(100), Required]
        public string Name { get; set; }
        [MaxLength(100), Required]
        public string Email { get; set; }
        [MaxLength(200), Required]
        public string Password { get; set; }

        #region Relations Properties

        public int IdRol { get; set; }

        #endregion

        public Rol RolNavigation { get; set; }
    }
}
