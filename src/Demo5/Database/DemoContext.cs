using Database.Configuration;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DemoContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Rol> Rol { get; set; }

        protected string ConnectionString { get; set; }

        public DemoContext(DbContextOptions<DemoContext> options) : base(options)
        {
        }

        protected DemoContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration())
                        .ApplyConfiguration(new RolConfiguration());
        }
    }
}
