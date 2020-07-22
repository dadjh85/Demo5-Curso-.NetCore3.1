using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Database.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        #region Relations Properties

        public ICollection<BookCategory> BookCategories { get; set; }

        #endregion
    }
}
