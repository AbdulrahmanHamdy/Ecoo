using ECOO.Application.Interfaces.Repositories;
using ECOO.Domain.Entities;
using ECOO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECOO.Infrastructure.Repositories;

/// <summary>
/// Concrete category repository.
/// </summary>
public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(AppDbContext context) : base(context) { }

    /// <inheritdoc/>
    public async Task<IEnumerable<Category>> GetAllOrderedAsync()
        => await _context.Categories
                         .AsNoTracking()
                         .OrderBy(c => c.Name)
                         .ToListAsync();
}
