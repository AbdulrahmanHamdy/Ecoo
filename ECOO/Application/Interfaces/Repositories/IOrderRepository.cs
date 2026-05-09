using ECOO.Domain.Entities;

namespace ECOO.Application.Interfaces.Repositories;


public interface IOrderRepository : IGenericRepository<Order>
{
    
    Task<Order?> GetByIdWithItemsAsync(int id);

    Task<IEnumerable<Order>> GetAllOrderedByDateAsync();
}
