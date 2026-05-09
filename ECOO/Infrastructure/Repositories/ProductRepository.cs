using ECOO.Application.Interfaces.Repositories;
using ECOO.Domain.Entities;
using ECOO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECOO.Infrastructure.Repositories;


public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    
    public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        => await _context.Products
                         .Include(p => p.Category)
                         .AsNoTracking()
                         .ToListAsync();

   
    public async Task<Product?> GetByIdWithCategoryAsync(int id)
        => await _context.Products
                         .Include(p => p.Category)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(p => p.Id == id);

  
    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        => await _context.Products
                         .Include(p => p.Category)
                         .AsNoTracking()
                         .Where(p => p.CategoryId == categoryId)
                         .ToListAsync();

    
    public async Task<IEnumerable<Product>> SearchAsync(string query)
    {
        var lower = query.ToLower();
        return await _context.Products
                             .Include(p => p.Category)
                             .AsNoTracking()
                             .Where(p => p.Name.ToLower().Contains(lower) ||
                                         p.Description.ToLower().Contains(lower))
                             .ToListAsync();
    }
}
