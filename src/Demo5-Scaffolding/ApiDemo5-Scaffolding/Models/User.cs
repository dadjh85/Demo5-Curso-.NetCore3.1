using System;
using System.Collections.Generic;

namespace ApiDemo5_Scaffolding.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public int IdRol { get; set; }

        public virtual Rol IdRolNavigation { get; set; }
        public virtual Client Client { get; set; }
    }
}
