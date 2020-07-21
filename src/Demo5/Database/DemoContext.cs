using Database.Configuration;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DemoContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }

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
                        .ApplyConfiguration(new RolConfiguration())
                        .ApplyConfiguration(new ClientConfiguration())
                        .ApplyConfiguration(new BookConfiguration())
                        .ApplyConfiguration(new CategoryConfiguration())
                        .ApplyConfiguration(new BookCategoryConfiguration());
        }
    }
}
