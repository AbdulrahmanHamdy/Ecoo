using ECOO.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECOO.Presentation.Controllers;

/// <summary>
/// Manages the session-based shopping cart.
/// All operations redirect back to either the cart page or the referring page.
/// </summary>
public class CartController : Controller
{
    private readonly ICartService    _cartService;
    private readonly IProductService _productService;

    public CartController(ICartService cartService, IProductService productService)
    {
        _cartService    = cartService;
        _productService = productService;
    }

    // GET /Cart
    public IActionResult Index()
        => View(_cartService.GetCart());

    // POST /Cart/Add
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Add(int productId, string? returnUrl)
    {
        var product = await _productService.GetProductByIdAsync(productId);
        if (product is null) return NotFound();

        _cartService.AddToCart(product.Id, product.Name, product.Price, product.ImageUrl);
        TempData["Success"] = $"\"{product.Name}\" was added to your cart.";

        // Redirect back to where the user came from (product listing / detail)
        if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction(nameof(Index));
    }

    // POST /Cart/Update
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Update(int productId, int quantity)
    {
        _cartService.UpdateQuantity(productId, quantity);
        return RedirectToAction(nameof(Index));
    }

    // POST /Cart/Remove
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Remove(int productId)
    {
        _cartService.RemoveFromCart(productId);
        TempData["Success"] = "Item removed from cart.";
        return RedirectToAction(nameof(Index));
    }

    // POST /Cart/Clear
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Clear()
    {
        _cartService.ClearCart();
        TempData["Success"] = "Your cart has been cleared.";
        return RedirectToAction(nameof(Index));
    }
}
