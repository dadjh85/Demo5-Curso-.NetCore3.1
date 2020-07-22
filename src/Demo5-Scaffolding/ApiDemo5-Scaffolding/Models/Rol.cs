using System;
using System.Collections.Generic;

namespace ApiDemo5_Scaffolding.Models
{
    public partial class Rol
    {
        public Rol()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
