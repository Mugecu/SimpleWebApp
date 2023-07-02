using Microsoft.EntityFrameworkCore;
using SimpleWebApp.Domain.Entities;
using SimpleWebApp.Domain.Interfaces;

namespace SimpleWebApp.Infrastructure.Repositories
{
    public class ProductEfRepository : ProductRepository
    {
        private readonly SimpleWebAppContext _context;
        public ProductEfRepository(SimpleWebAppContext context)
        {
            _context = context;
        }

        public override async Task<List<KeyValuePair<Guid, string>>> ContainceProductInRepository(IEnumerable<KeyValuePair<Guid, string>> productIds)
        { 
            var availableProducts =  await _context.Set<Product>()
                .Where(i => productIds.Select(a => a.Key).ToList().Contains(i.Id))
                .ToListAsync();

            var productsNotInRepository = productIds
                .Join(availableProducts,
                    p => p.Key,
                    available => available.Id,
                    (p, available) => p);

            return productsNotInRepository.ToList();
        }

        public override async Task<Product> CreateAsync(Product root)
        {
            return (await _context.Set<Product>().AddAsync(root)).Entity;
        }

        public override async Task<Product?> GetAsync(Guid id)
        {
            return await _context.Set<Product>().FirstOrDefaultAsync(p => p.Id == id);
        }

        public override async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
