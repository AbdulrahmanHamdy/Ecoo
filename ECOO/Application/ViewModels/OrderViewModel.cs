namespace ECOO.Application.ViewModels;

/// <summary>
/// ViewModel for displaying a completed order (confirmation / history).
/// </summary>
public class OrderViewModel
{
    public int Id { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string CustomerName { get; set; } = string.Empty;
    public string CustomerEmail { get; set; } = string.Empty;
    public string ShippingAddress { get; set; } = string.Empty;
    public List<OrderItemViewModel> Items { get; set; } = new();
}

/// <summary>
/// ViewModel for a single line item in an order detail view.
/// </summary>
public class OrderItemViewModel
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty;
    public string ProductImageUrl { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public decimal LineTotal => Price * Quantity;
}
