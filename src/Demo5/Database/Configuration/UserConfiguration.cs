using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(e => e.RolNavigation)
                   .WithMany(e => e.Users)
                   .HasForeignKey(e => e.IdRol);

            //builder.Property(e => e.IsActive).HasDefaultValue(true);

            builder.HasQueryFilter(e => e.IsActive);
        }
    }
}
