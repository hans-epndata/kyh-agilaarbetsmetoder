using Microsoft.EntityFrameworkCore;
using WebApi.Contexts;
using WebApi.Services;

namespace WebApi.Repositories
{
    public class ProductRepository
    {
        private readonly DataContext _context;

        public ProductRepository(DataContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Product>> GetAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAsync(int take)
        {
            return await _context.Products.Take(take).ToListAsync();
        }
    }
}
