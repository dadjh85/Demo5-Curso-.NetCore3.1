using Database.Configuration;
using Database.Model;
using Microsoft.EntityFrameworkCore;

namespace Database
{
    public class DemoContext : DbContext
    {
        #region Models

        public DbSet<User> User { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }

        #endregion

        protected string ConnectionString { get; set; }

        protected DemoContext(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public DemoContext(DbContextOptions<DemoContext> options) : base (options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration())
                        .ApplyConfiguration(new RolConfiguration())
                        .ApplyConfiguration(new BookConfiguration())
                        .ApplyConfiguration(new CategoryConfiguration())
                        .ApplyConfiguration(new BookCategoryConfiguration());
        }
    }
}
