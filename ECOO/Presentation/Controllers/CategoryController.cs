using ECOO.Application.Interfaces.Services;
using ECOO.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace ECOO.Presentation.Controllers;

/// <summary>
/// Full CRUD for product categories.
/// </summary>
public class CategoryController : Controller
{
    private readonly ICategoryService _categoryService;

    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    // GET /Category
    public async Task<IActionResult> Index()
        => View(await _categoryService.GetAllCategoriesOrderedAsync());

    // GET /Category/Create
    public IActionResult Create() => View(new CategoryViewModel());

    // POST /Category/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CategoryViewModel model)
    {
        if (!ModelState.IsValid) return View(model);

        await _categoryService.CreateCategoryAsync(model);
        TempData["Success"] = $"Category \"{model.Name}\" was created.";
        return RedirectToAction(nameof(Index));
    }

    // GET /Category/Edit/5
    public async Task<IActionResult> Edit(int id)
    {
        var cat = await _categoryService.GetCategoryByIdAsync(id);
        if (cat is null) return NotFound();
        return View(cat);
    }

    // POST /Category/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, CategoryViewModel model)
    {
        if (id != model.Id) return BadRequest();
        if (!ModelState.IsValid) return View(model);

        await _categoryService.UpdateCategoryAsync(model);
        TempData["Success"] = $"Category \"{model.Name}\" was updated.";
        return RedirectToAction(nameof(Index));
    }

    // GET /Category/Delete/5
    public async Task<IActionResult> Delete(int id)
    {
        var cat = await _categoryService.GetCategoryByIdAsync(id);
        if (cat is null) return NotFound();
        return View(cat);
    }

    // POST /Category/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _categoryService.DeleteCategoryAsync(id);
        TempData["Success"] = "Category deleted.";
        return RedirectToAction(nameof(Index));
    }
}
