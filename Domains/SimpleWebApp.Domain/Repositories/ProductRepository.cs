using SimpleWebApp.Domain.Abstracts;
using SimpleWebApp.Domain.Entities;

namespace SimpleWebApp.Domain.Interfaces
{
    public abstract class ProductRepository : Repository<Product>
    {
        public abstract Task<List<KeyValuePair<Guid, string>>> ContainceProductInRepository(IEnumerable<KeyValuePair<Guid, string>> productIds);
    }
}
