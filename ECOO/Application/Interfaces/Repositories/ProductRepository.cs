using ECOO.Application.Interfaces.Repositories;
using ECOO.Domain.Entities;
using ECOO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECOO.Infrastructure.Repositories;

/// <summary>
/// Concrete product repository.
/// Overrides / extends the generic base with product-specific queries
/// that require eager-loading of the Category navigation property.
/// </summary>
public class ProductRepository : GenericRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext context) : base(context) { }

    /// <inheritdoc/>
    public async Task<IEnumerable<Product>> GetAllWithCategoryAsync()
        => await _context.Products
                         .Include(p => p.Category)
                         .AsNoTracking()
                         .ToListAsync();

    /// <inheritdoc/>
    public async Task<Product?> GetByIdWithCategoryAsync(int id)
        => await _context.Products
                         .Include(p => p.Category)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(p => p.Id == id);

    /// <inheritdoc/>
    public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        => await _context.Products
                         .Include(p => p.Category)
                         .AsNoTracking()
                         .Where(p => p.CategoryId == categoryId)
                         .ToListAsync();

    /// <inheritdoc/>
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
