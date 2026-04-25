using ECOO.Domain.Entities;

namespace ECOO.Application.Interfaces.Repositories;

/// <summary>
/// Order-specific repository contract.
/// Provides queries that eagerly load order items and their products.
/// </summary>
public interface IOrderRepository : IGenericRepository<Order>
{
    /// <summary>Returns an order with its OrderItems (and each item's Product) loaded.</summary>
    Task<Order?> GetByIdWithItemsAsync(int id);

    /// <summary>Returns all orders, most recent first.</summary>
    Task<IEnumerable<Order>> GetAllOrderedByDateAsync();
}
