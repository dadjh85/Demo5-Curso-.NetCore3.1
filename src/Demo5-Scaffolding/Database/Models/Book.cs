using System;
using System.Collections.Generic;

namespace Database.Models
{
    public partial class Book
    {
        public Book()
        {
            BookCategory = new HashSet<BookCategory>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        public virtual ICollection<BookCategory> BookCategory { get; set; }
    }
}
