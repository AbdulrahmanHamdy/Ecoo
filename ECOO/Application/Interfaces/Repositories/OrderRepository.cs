using ECOO.Application.Interfaces.Repositories;
using ECOO.Domain.Entities;
using ECOO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECOO.Infrastructure.Repositories;

/// <summary>
/// Concrete order repository.
/// Loads the full order graph (items + products) for detail/confirmation views.
/// </summary>
public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

    /// <inheritdoc/>
    public async Task<Order?> GetByIdWithItemsAsync(int id)
        => await _context.Orders
                         .Include(o => o.OrderItems)
                             .ThenInclude(oi => oi.Product)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(o => o.Id == id);

    /// <inheritdoc/>
    public async Task<IEnumerable<Order>> GetAllOrderedByDateAsync()
        => await _context.Orders
                         .AsNoTracking()
                         .OrderByDescending(o => o.OrderDate)
                         .ToListAsync();
}
