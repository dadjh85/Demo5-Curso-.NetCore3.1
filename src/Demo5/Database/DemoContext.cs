using Database.Configuration;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;

namespace Database
{
    public class DemoContext : DbContext
    {
        #region Models

        public DbSet<User> User { get; set; }
        public DbSet<Rol> Rol { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Book> Book { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<BookCategory> BookCategory { get; set; }

        #endregion


        #region Properties

        protected string ConnectionString { get; set; }

        public static readonly ILoggerFactory EfCoreLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information);
            builder.AddDebug();
        });

        #endregion

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

            optionsBuilder.UseLoggerFactory(EfCoreLoggerFactory);
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
