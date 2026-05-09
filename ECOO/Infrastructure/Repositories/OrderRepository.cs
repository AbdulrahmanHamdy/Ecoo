using ECOO.Application.Interfaces.Repositories;
using ECOO.Domain.Entities;
using ECOO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECOO.Infrastructure.Repositories;


public class OrderRepository : GenericRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext context) : base(context) { }

   
    public async Task<Order?> GetByIdWithItemsAsync(int id)
        => await _context.Orders
                         .Include(o => o.OrderItems)
                             .ThenInclude(oi => oi.Product)
                         .AsNoTracking()
                         .FirstOrDefaultAsync(o => o.Id == id);

   
    public async Task<IEnumerable<Order>> GetAllOrderedByDateAsync()
        => await _context.Orders
                         .AsNoTracking()
                         .OrderByDescending(o => o.OrderDate)
                         .ToListAsync();
}
