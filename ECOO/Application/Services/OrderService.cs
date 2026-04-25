using ECOO.Application.Interfaces.Repositories;
using ECOO.Application.Interfaces.Services;
using ECOO.Application.ViewModels;
using ECOO.Domain.Entities;

namespace ECOO.Application.Services;

/// <summary>
/// Handles order placement and retrieval.
/// Converts the session cart into persistent Order + OrderItem records.
/// </summary>
public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepo;
    private readonly ICartService     _cartService;

    public OrderService(IOrderRepository orderRepo, ICartService cartService)
    {
        _orderRepo   = orderRepo;
        _cartService = cartService;
    }

    // ── Commands ───────────────────────────────────────────────────

    /// <inheritdoc/>
    public async Task<int> PlaceOrderAsync(CheckoutViewModel model)
    {
        var cart = _cartService.GetCart();

        if (!cart.Items.Any())
            throw new InvalidOperationException("Cannot place an order with an empty cart.");

        // Build the Order entity from the checkout form data
        var order = new Order
        {
            OrderDate       = DateTime.UtcNow,
            TotalAmount     = cart.Total,
            CustomerName    = model.CustomerName,
            CustomerEmail   = model.CustomerEmail,
            ShippingAddress = model.ShippingAddress,
            OrderItems      = cart.Items.Select(i => new OrderItem
            {
                ProductId = i.ProductId,
                Quantity  = i.Quantity,
                Price     = i.Price           // snapshot price at time of purchase
            }).ToList()
        };

        await _orderRepo.AddAsync(order);

        // Clear the cart after successful order placement
        _cartService.ClearCart();

        return order.Id;
    }

    // ── Queries ────────────────────────────────────────────────────

    /// <inheritdoc/>
    public async Task<OrderViewModel?> GetOrderByIdAsync(int id)
    {
        var order = await _orderRepo.GetByIdWithItemsAsync(id);
        return order is null ? null : MapToViewModel(order);
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<OrderViewModel>> GetAllOrdersAsync()
    {
        var orders = await _orderRepo.GetAllOrderedByDateAsync();
        return orders.Select(o => MapToViewModel(o));
    }

    // ── Mapping helpers ────────────────────────────────────────────

    private static OrderViewModel MapToViewModel(Order o) => new()
    {
        Id              = o.Id,
        OrderDate       = o.OrderDate,
        TotalAmount     = o.TotalAmount,
        CustomerName    = o.CustomerName,
        CustomerEmail   = o.CustomerEmail,
        ShippingAddress = o.ShippingAddress,
        Items           = o.OrderItems.Select(oi => new OrderItemViewModel
        {
            ProductId       = oi.ProductId,
            ProductName     = oi.Product?.Name     ?? "Unknown",
            ProductImageUrl = oi.Product?.ImageUrl ?? string.Empty,
            Quantity        = oi.Quantity,
            Price           = oi.Price
        }).ToList()
    };
}
