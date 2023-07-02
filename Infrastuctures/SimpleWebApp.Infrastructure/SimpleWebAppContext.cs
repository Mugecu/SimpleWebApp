using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SimpleWebApp.Domain.Entities;

namespace SimpleWebApp.Infrastructure
{
    public class SimpleWebAppContext : DbContext
    {
        public SimpleWebAppContext(DbContextOptions<SimpleWebAppContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var jsonSettings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.Auto };
            modelBuilder.Entity<Product>().HasKey(i => i.Id);

            modelBuilder.Entity<Warehouse>().HasKey(i => i.Id);
            modelBuilder.Entity<Warehouse>()
                .Property(p => p.RegistredProducts)
                .HasConversion(p => JsonConvert.SerializeObject(p, Formatting.Indented, jsonSettings),
                               p => JsonConvert.DeserializeObject<List<KeyValuePair<KeyValuePair<Guid, string>, uint>>>(p, jsonSettings));

            base.OnModelCreating(modelBuilder);
        }
    }
}
