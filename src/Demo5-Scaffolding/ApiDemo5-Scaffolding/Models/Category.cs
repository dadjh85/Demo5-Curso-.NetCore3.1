using System;
using System.Collections.Generic;

namespace ApiDemo5_Scaffolding.Models
{
    public partial class Category
    {
        public Category()
        {
            BookCategory = new HashSet<BookCategory>();
        }

        public int Id { get; set; }
        public string CategoryName { get; set; }

        public virtual ICollection<BookCategory> BookCategory { get; set; }
    }
}
