using ECOO.Application.ViewModels;

namespace ECOO.Application.Interfaces.Services;

/// <summary>
/// Business-logic contract for order operations.
/// </summary>
public interface IOrderService
{
    /// <summary>Creates an order from the checkout form + current cart, returns the new order id.</summary>
    Task<int> PlaceOrderAsync(CheckoutViewModel model);

    Task<OrderViewModel?> GetOrderByIdAsync(int id);
    Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync();
}
