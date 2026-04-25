using ECOO.Application.Interfaces.Services;
using ECOO.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECOO.Presentation.Controllers;

/// <summary>
/// Full CRUD for products — list, detail, create, edit, delete.
/// </summary>
public class ProductController : Controller
{
    private readonly IProductService  _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService  = productService;
        _categoryService = categoryService;
    }

    // ── List ───────────────────────────────────────────────────────

    // GET /Product
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsWithCategoryAsync();
        return View(products);
    }

    // ── Detail ─────────────────────────────────────────────────────

    // GET /Product/Details/5
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetProductByIdWithCategoryAsync(id);
        if (product is null) return NotFound();
        return View(product);
    }

    // ── Create ─────────────────────────────────────────────────────

    // GET /Product/Create
    public async Task<IActionResult> Create()
    {
        var vm = new ProductViewModel
        {
            Categories = await BuildCategorySelectListAsync()
        };
        return View(vm);
    }

    // POST /Product/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductViewModel model)
    {
        if (!ModelState.IsValid)
        {
            model.Categories = await BuildCategorySelectListAsync(model.CategoryId);
            return View(model);
        }

        await _productService.CreateProductAsync(model);
        TempData["Success"] = $"Product \"{model.Name}\" was created successfully.";
        return RedirectToAction(nameof(Index));
    }

    // ── Edit ───────────────────────────────────────────────────────

    // GET /Product/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product is null) return NotFound();

        product.Categories = await BuildCategorySelectListAsync(product.CategoryId);
        return View(product);
    }

    // POST /Product/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ProductViewModel model)
    {
        if (id != model.Id) return BadRequest();

        if (!ModelState.IsValid)
        {
            model.Categories = await BuildCategorySelectListAsync(model.CategoryId);
            return View(model);
        }

        await _productService.UpdateProductAsync(model);
        TempData["Success"] = $"Product \"{model.Name}\" was updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    // ── Delete ─────────────────────────────────────────────────────

    // GET /Product/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductByIdWithCategoryAsync(id);
        if (product is null) return NotFound();
        return View(product);
    }

    // POST /Product/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.DeleteProductAsync(id);
        TempData["Success"] = "Product deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    // ── Helpers ────────────────────────────────────────────────────

    /// <summary>Builds the category SelectList for create/edit forms.</summary>
    private async Task<SelectList> BuildCategorySelectListAsync(int selectedId = 0)
    {
        var categories = await _categoryService.GetAllCategoriesOrderedAsync();
        return new SelectList(categories, "Id", "Name", selectedId);
    }
}
