using Microsoft.EntityFrameworkCore;
using SimpleWebApp.Domain.Abstracts;
using SimpleWebApp.Domain.Entities;

namespace SimpleWebApp.Infrastructure.Repositories
{
    public class WarehouseEfRepository : Repository<Warehouse>
    {
        private readonly SimpleWebAppContext _context;

        public WarehouseEfRepository(SimpleWebAppContext context)
        {
            _context = context;
        }

        public override async Task<Warehouse> CreateAsync(Warehouse root)
        {
            var warehouse = (await _context.Set<Warehouse>().AddAsync(root)).Entity;
            return warehouse;
        }

        public override async Task<Warehouse?> GetAsync(Guid id)
        {
            return await _context.Set<Warehouse>().FirstOrDefaultAsync(w => w.Id == id);
        }

        public override async Task SaveAsync()
            => await _context.SaveChangesAsync();
    }
}
