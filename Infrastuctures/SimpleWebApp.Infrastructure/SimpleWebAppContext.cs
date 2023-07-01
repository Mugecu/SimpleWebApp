using Microsoft.EntityFrameworkCore;
using SimpleWebApp.Domain.Entities;

namespace SimpleWebApp.Infrastructure
{
    public class SimpleWebAppContext : DbContext
    {
        public virtual DbSet<Product> Products { get; set; }

        public SimpleWebAppContext(DbContextOptions<SimpleWebAppContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasKey(i => i.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
