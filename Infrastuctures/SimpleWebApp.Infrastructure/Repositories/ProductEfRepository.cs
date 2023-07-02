using Microsoft.EntityFrameworkCore;
using SimpleWebApp.Domain.Abstracts;
using SimpleWebApp.Domain.Entities;

namespace SimpleWebApp.Infrastructure.Repositories
{
    public class ProductEfRepository : Repository<Product>
    {
        private readonly SimpleWebAppContext _context;
        public ProductEfRepository(SimpleWebAppContext context)
        {
            _context = context;
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
