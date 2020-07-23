using Database.Configuration;
using Database.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Debug;
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

        /// <summary>
        /// Method SaveChangesAsync with validation of model
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default(CancellationToken))
        {
            ModelValidation();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Method SaveChanges with validation of model
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess"></param>
        /// <returns></returns>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            ModelValidation();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        #region Private methods

        /// <summary>
        /// Method that validate Model of Entity Frameork before save changes in database
        /// </summary>
        private void ModelValidation()
        {
            foreach (EntityEntry recordToValidate in ChangeTracker.Entries())
            {
                object entity = recordToValidate.Entity;
                ValidationContext validationContext = new ValidationContext(entity);
                var results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(entity, validationContext, results, true))
                {
                    var messages = results.Select(r => r.ErrorMessage).Aggregate((message, nextMessage) => message + ", " + nextMessage);
                    throw new InvalidOperationException($"Unable to save changes for {entity.GetType().FullName} due to error(s): {messages}");
                }
            }
        }

        #endregion
    }
}
