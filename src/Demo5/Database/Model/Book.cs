using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(50), Required]
        public string Title { get; set; }
        public string Author { get; set; }

        #region Relations

        public ICollection<BookCategory> BookCategories { get; set; }

        #endregion
    }
}
