using ECOO.Application.Interfaces.Services;
using ECOO.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECOO.Presentation.Controllers;


public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ICartService  _cartService;

    public OrderController(IOrderService orderService, ICartService cartService)
    {
        _orderService = orderService;
        _cartService  = cartService;
    }

    public IActionResult Checkout()
    {
        var cart = _cartService.GetCart();

        
        if (!cart.Items.Any())
        {
            TempData["Warning"] = "Your cart is empty. Add some products before checking out.";
            return RedirectToAction("Index", "Cart");
        }

        var vm = new CheckoutViewModel { Cart = cart };
        return View(vm);
    }

   
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
        
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

  
    public async Task<IActionResult> Confirmation(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order is null) return NotFound();
        return View(order);
    }

  
    public async Task<IActionResult> Index()
        => View(await _orderService.GetAllOrdersAsync());

   
    public async Task<IActionResult> Details(int id)
    {
        var order = await _orderService.GetOrderByIdAsync(id);
        if (order is null) return NotFound();
        return View(order);
    }
}
