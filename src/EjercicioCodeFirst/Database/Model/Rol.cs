using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Rol
    {
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string Name { get; set; }
        public string Description { get; set; }

        #region Relations Properties

        public ICollection<User> Users { get; set; }

        #endregion
    }
}
