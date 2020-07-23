using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Database.Configuration
{
    internal class ClientConfiguration : IEntityTypeConfiguration<Client>
    {
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.HasOne(e => e.UserNavigation)
                   .WithOne(e => e.ClientNavigation)
                   .HasForeignKey<Client>(e => e.IdUser);
        }
    }
}
