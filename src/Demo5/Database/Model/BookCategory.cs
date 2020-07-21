

namespace Database.Model
{
    public class BookCategory
    {
        public int BookId { get; set; }

        public int CategoryId { get; set; }

        #region Relations

        public Book Book { get; set; }

        public Category Category { get; set; }

        #endregion
    }
}
