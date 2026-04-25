using ECOO.Application.ViewModels;

namespace ECOO.Application.Interfaces.Services;

/// <summary>
/// Business-logic contract for session-based shopping cart operations.
/// All cart state lives in ISession — no database writes occur here.
/// </summary>
public interface ICartService
{
    /// <summary>Returns the current cart from session (empty cart if no session exists).</summary>
    CartViewModel GetCart();

    /// <summary>Adds one unit of a product to the cart, or increments quantity if already present.</summary>
    void AddToCart(int productId, string name, decimal price, string imageUrl);

    /// <summary>Updates the quantity of a specific cart line. Removes the line if quantity ≤ 0.</summary>
    void UpdateQuantity(int productId, int quantity);

    /// <summary>Removes a product from the cart entirely.</summary>
    void RemoveFromCart(int productId);

    /// <summary>Empties the cart and removes the session key.</summary>
    void ClearCart();

    /// <summary>Returns the total number of items (sum of all quantities) — used in the navbar badge.</summary>
    int GetCartItemCount();
}
