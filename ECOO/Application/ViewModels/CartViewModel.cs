namespace ECOO.Application.ViewModels;


public class CartItemViewModel
{
    public int ProductId    { get; set; }
    public string Name      { get; set; } = string.Empty;
    public decimal Price    { get; set; }
    public int Quantity     { get; set; }
    public string ImageUrl  { get; set; } = string.Empty;

    
    public decimal LineTotal => Price * Quantity;
}


public class CartViewModel
{
    public List<CartItemViewModel> Items { get; set; } = new();

    
    public decimal Total => Items.Sum(i => i.LineTotal);

  
    public int ItemCount => Items.Sum(i => i.Quantity);
}
