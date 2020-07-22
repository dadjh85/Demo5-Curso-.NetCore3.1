using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration
{
    internal class BookCategoryConfiguration : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.HasKey(e => new { e.BookId, e.CategoryId });

            builder.HasOne(e => e.BookNavigation)
                   .WithMany(e => e.BookCategories)
                   .HasForeignKey(e => e.BookId);

            builder.HasOne(e => e.CategoryNavigation)
                   .WithMany(e => e.BookCategories)
                   .HasForeignKey(e => e.CategoryId);
        }
    }
}
