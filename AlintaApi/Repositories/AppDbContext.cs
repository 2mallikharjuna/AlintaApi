using AlintaApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace AlintaApi.Repositories
{
    /// <summary>
    /// Common application DB Context
    /// </summary>
    public sealed class AppDbContext : DbContext
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="options"></param>
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        /// <summary>
        /// Override Model creation
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);

            // Apply configurations for entity
            modelBuilder.ApplyConfiguration(new Customer());
           
        }
    }
}
