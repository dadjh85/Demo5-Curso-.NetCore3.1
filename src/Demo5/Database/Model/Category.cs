using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Category
    {
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string  CategoryName { get; set; }

        #region Relations

        public ICollection<BookCategory> BookCategories { get; set; }

        #endregion
    }
}
