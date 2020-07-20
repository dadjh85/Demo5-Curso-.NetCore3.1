using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Rol
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }

        #region Inverse Properties

        public ICollection<User> Users { get; set; }

        #endregion
    }
}
