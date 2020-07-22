using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Model
{
    public class BookCategory
    {
        public int BookId { get; set; }
        public int CategoryId { get; set; }

        #region Relations

        public Book BookNavigation { get; set; }

        public Category CategoryNavigation { get; set; }

        #endregion
    }
}
