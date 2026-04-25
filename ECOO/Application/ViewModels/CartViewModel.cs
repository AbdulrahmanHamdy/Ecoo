namespace ECOO.Application.ViewModels;

/// <summary>
/// Represents a single line item in the shopping cart.
/// Stored as a list inside CartViewModel (serialised to session).
/// </summary>
public class CartItemViewModel
{
    public int ProductId    { get; set; }
    public string Name      { get; set; } = string.Empty;
    public decimal Price    { get; set; }
    public int Quantity     { get; set; }
    public string ImageUrl  { get; set; } = string.Empty;

    /// <summary>Computed line total (Price × Quantity).</summary>
    public decimal LineTotal => Price * Quantity;
}

/// <summary>
/// Represents the entire shopping cart, passed to the cart view.
/// </summary>
public class CartViewModel
{
    public List<CartItemViewModel> Items { get; set; } = new();

    /// <summary>Grand total of all line items.</summary>
    public decimal Total => Items.Sum(i => i.LineTotal);

    /// <summary>Total number of individual products (sum of all quantities).</summary>
    public int ItemCount => Items.Sum(i => i.Quantity);
}
