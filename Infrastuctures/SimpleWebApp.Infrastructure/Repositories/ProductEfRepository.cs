using SimpleWebApp.Domain.Abstracts;
using SimpleWebApp.Domain.Entities;

namespace SimpleWebApp.Infrastructure.Repositories
{
    public class ProductEfRepository : Repository<Product>
    {
        public override Task<Product> CreateAsync(Product root)
        {
            throw new NotImplementedException();
        }

        public override Task<Product?> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public override Task SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
