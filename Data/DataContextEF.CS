using csharpstarterapp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace csharpstarterapp.Data
{
    /// <summary>
    /// Represents the data context for the application using Entity Framework Core.
    /// This context is responsible for interacting with the database and provides access to the 
    /// <see cref="Computer"/> entities.
    /// </summary>
    public class DataContextEF : DbContext


    {

        private IConfiguration _config;

        public DataContextEF(IConfiguration config) {
           // _config = config;
            _config = config;
            
        }


        /// <summary>
        /// Gets or sets the <see cref="DbSet{Computer}"/> representing the collection of 
        /// <see cref="Computer"/> entities in the database.
        /// </summary>
        public DbSet<Computer>? Computer { get; set; }

        /// <summary>
        /// Configures the database context to connect to a SQL Server database.
        /// This method is called when the context is being created.
        /// </summary>
        /// <param name="options">The options builder to configure the context.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
            {
                // Configures the context to use SQL Server with the specified connection string
                options.UseSqlServer(_config.GetConnectionString("DefaultConnection"), 
                    options => options.EnableRetryOnFailure());
            }
        }

        /// <summary>
        /// Configures the model for the context using the specified <see cref="ModelBuilder"/>.
        /// This method is called when the model for the context is being created.
        /// </summary>
        /// <param name="modelBuilder">The builder to configure the model.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Sets the default schema for the database tables
            modelBuilder.HasDefaultSchema("StarterAppSchema");

            // Configures the key for the Computer entity
            modelBuilder.Entity<Computer>()
                .HasKey(c => c.ComputerId);
                //.ToTable("Computer", "StarterAppSchema"); // Uncomment to specify the table name and schema explicitly
                //.ToTable("TableName", "SchemaName"); // Example for specifying a different table and schema name
        }
    }
}
