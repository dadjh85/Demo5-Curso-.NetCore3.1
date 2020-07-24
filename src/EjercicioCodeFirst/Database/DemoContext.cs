using Database.Configuration;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
        public DbSet<Client> Client { get; set; }

        #endregion

        protected string ConnectionString { get; set; }

        public static readonly ILoggerFactory EFCoreLoggerFactory = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information);
            builder.AddDebug();
        });

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

            optionsBuilder.UseLoggerFactory(EFCoreLoggerFactory);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration())
                        .ApplyConfiguration(new RolConfiguration())
                        .ApplyConfiguration(new BookConfiguration())
                        .ApplyConfiguration(new CategoryConfiguration())
                        .ApplyConfiguration(new BookCategoryConfiguration())
                        .ApplyConfiguration(new ClientConfiguration());
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ModelValidation();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ModelValidation();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        #region Private methods

        private void ModelValidation()
        {
            foreach(EntityEntry recordToValidate in ChangeTracker.Entries())
            {
                object entity = recordToValidate.Entity;
                ValidationContext validation = new ValidationContext(entity);
                var results = new List<ValidationResult>();
                if(!Validator.TryValidateObject(entity, validation, results, true))
                {
                    var listmessages = results.Select(r => r.ErrorMessage).Aggregate((message, nextMessage) => message + ", " + nextMessage);
                    throw new InvalidOperationException($"Error validación modelo EF: {entity.GetType().FullName} mensaje: {listmessages}"); 
                }
            }
        }

        #endregion
    }
}
