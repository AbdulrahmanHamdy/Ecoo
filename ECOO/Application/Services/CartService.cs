using ECOO.Application.Interfaces.Services;
using ECOO.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECOO.Application.Services;


public class CartService : ICartService
{
    private const string CartSessionKey = "ECOO_Cart";

    private readonly IHttpContextAccessor _httpContextAccessor;

    
    private ISession Session => _httpContextAccessor.HttpContext!.Session;

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

  
    public CartViewModel GetCart()
    {
        var json = Session.GetString(CartSessionKey);

        if (string.IsNullOrEmpty(json))
            return new CartViewModel { Items = new List<CartItemViewModel>() };

        var cart = JsonConvert.DeserializeObject<CartViewModel>(json);

        return cart ?? new CartViewModel { Items = new List<CartItemViewModel>() };
    }

    
    public int GetCartItemCount()
        => GetCart().ItemCount;

   
    public void AddToCart(int productId, string name, decimal price, string imageUrl)
    {
        var cart = GetCart();
        var existing = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (existing is not null)
        {
            existing.Quantity++;
        }
        else
        {
            cart.Items.Add(new CartItemViewModel
            {
                ProductId = productId,
                Name      = name,
                Price     = price,
                Quantity  = 1,
                ImageUrl  = imageUrl
            });
        }

        SaveCart(cart);
    }

   
    public void UpdateQuantity(int productId, int quantity)
    {
        var cart = GetCart();
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);

        if (item is null) return;

        if (quantity <= 0)
            cart.Items.Remove(item);
        else
            item.Quantity = quantity;

        SaveCart(cart);
    }

    
    public void RemoveFromCart(int productId)
    {
        var cart = GetCart();
        var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
        if (item is not null)
        {
            cart.Items.Remove(item);
            SaveCart(cart);
        }
    }

    
    public void ClearCart()
        => Session.Remove(CartSessionKey);

   

    private void SaveCart(CartViewModel cart)
        => Session.SetString(CartSessionKey, JsonConvert.SerializeObject(cart));
}
