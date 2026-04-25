using ECOO.Application.Interfaces.Services;
using ECOO.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECOO.Presentation.Controllers;

/// <summary>
/// Handles checkout flow and order confirmation.
/// </summary>
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ICartService  _cartService;

    public OrderController(IOrderService orderService, ICartService cartService)
    {
        _orderService = orderService;
        _cartService  = cartService;
    }

    // ── Checkout ───────────────────────────────────────────────────

    // GET /Order/Checkout
    public IActionResult Checkout()
    {
        var cart = _cartService.GetCart();

        // Don't allow checkout with an empty cart
        if (!cart.Items.Any())
        {
            TempData["Warning"] = "Your cart is empty. Add some products before checking out.";
            return RedirectToAction("Index", "Cart");
        }

        var vm = new CheckoutViewModel { Cart = cart };
        return View(vm);
    }

    // POST /Order/Checkout
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        // Re-attach the current cart (not bound from the form)
        model.Cart = _cartService.GetCart();

        if (!model.Cart.Items.Any())
        {
            TempData["Warning"] = "Your cart is empty.";
            return RedirectToAction("Index", "Cart");
        }

        if (!ModelState.IsValid)
            return View(model);

        try
        {
            var orderId = await _orderService.PlaceOrderAsync(model);
            return RedirectToAction(nameof(Confirmation), new { id = orderId });
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            return View(model);
        }
    }

    // ── Confirmation ───────────────────────────────────────────────

    // GET /Order/Confirmation/5
    public async Task<IActionResult> Confirmation(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order is null) return NotFound();
        return View(order);
    }

    // ── Order history (admin-friendly list) ────────────────────────

    // GET /Order
    public async Task<IActionResult> Index()
        => View(await _orderService.GetAllOrdersAsync());

    // GET /Order/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order is null) return NotFound();
        return View(order);
    }
}
