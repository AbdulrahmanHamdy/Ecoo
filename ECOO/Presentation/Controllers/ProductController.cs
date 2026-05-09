using ECOO.Application.Interfaces.Services;
using ECOO.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECOO.Presentation.Controllers;

public class ProductController : Controller
{
    private readonly IProductService  _productService;
    private readonly ICategoryService _categoryService;

    public ProductController(IProductService productService, ICategoryService categoryService)
    {
        _productService  = productService;
        _categoryService = categoryService;
    }

    
    public async Task<IActionResult> Index()
    {
        var products = await _productService.GetAllProductsWithCategoryAsync();
        return View(products);
    }

   
    public async Task<IActionResult> Details(int id)
    {
        var product = await _productService.GetProductByIdWithCategoryAsync(id);
        if (product is null) return NotFound();
        return View(product);
    }

  
    public async Task<IActionResult> Create()
    {
        var vm = new ProductViewModel
        {
            Categories = await BuildCategorySelectListAsync()
        };
        return View(vm);
    }

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

   
    public async Task<IActionResult> Edit(int id)
    {
        var product = await _productService.GetProductByIdAsync(id);
        if (product is null) return NotFound();

        product.Categories = await BuildCategorySelectListAsync(product.CategoryId);
        return View(product);
    }

    
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

    public async Task<IActionResult> Delete(int id)
    {
        var product = await _productService.GetProductByIdWithCategoryAsync(id);
        if (product is null) return NotFound();
        return View(product);
    }

    
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _productService.DeleteProductAsync(id);
        TempData["Success"] = "Product deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    
    private async Task<SelectList> BuildCategorySelectListAsync(int selectedId = 0)
    {
        var categories = await _categoryService.GetAllCategoriesOrderedAsync();
        return new SelectList(categories, "Id", "Name", selectedId);
    }
}
