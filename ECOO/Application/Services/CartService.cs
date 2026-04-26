using ECOO.Application.Interfaces.Services;
using ECOO.Application.ViewModels;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ECOO.Application.Services;

/// <summary>
/// Session-based cart service.
/// The entire cart is serialised as JSON and stored under a single session key.
/// No database is touched — cart state lives only in the HTTP session.
/// </summary>
public class CartService : ICartService
{
    private const string CartSessionKey = "ECOO_Cart";

    private readonly IHttpContextAccessor _httpContextAccessor;

    // Convenience property — throws if context is unavailable (should never happen in MVC)
    private ISession Session => _httpContextAccessor.HttpContext!.Session;

    public CartService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    // ── Read ───────────────────────────────────────────────────────

    /// <inheritdoc/>
    public CartViewModel GetCart()
    {
        var json = Session.GetString(CartSessionKey);

        if (string.IsNullOrEmpty(json))
            return new CartViewModel { Items = new List<CartItemViewModel>() };

        var cart = JsonConvert.DeserializeObject<CartViewModel>(json);

        return cart ?? new CartViewModel { Items = new List<CartItemViewModel>() };
    }

    /// <inheritdoc/>
    public int GetCartItemCount()
        => GetCart().ItemCount;

    // ── Write ──────────────────────────────────────────────────────

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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

    /// <inheritdoc/>
    public void ClearCart()
        => Session.Remove(CartSessionKey);

    // ── Private helpers ────────────────────────────────────────────

    private void SaveCart(CartViewModel cart)
        => Session.SetString(CartSessionKey, JsonConvert.SerializeObject(cart));
}
