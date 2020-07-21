using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration
{
    internal class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(bc => new { bc.BookId, bc.CategoryId });

            builder.HasOne(bc => bc.Book)
                   .WithMany(b => b.BookCategories)
                   .HasForeignKey(bc => bc.BookId);

            builder.HasOne(bc => bc.Category)
                   .WithMany(c => c.BookCategories)
                   .HasForeignKey(bc => bc.CategoryId);
        }
    }
}
