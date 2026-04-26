using ECOO.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECOO.Presentation.Controllers
{
    /// <summary>
    /// Handles the home/landing page.
    /// Shows the product catalogue with optional category filtering and search.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        /// <summary>
        /// Landing page — lists products filtered by category and/or search term.
        /// Both filters are optional; if neither is provided all products are shown.
        /// </summary>
        public async Task<IActionResult> Index(int? categoryId, string? search)
        {
            // Populate category filter nav (always sorted A→Z)
            var categories = await _categoryService.GetAllCategoriesOrderedAsync();
            ViewBag.Categories = categories;
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.Search = search;

            // Search takes priority; otherwise filter by category, or return all
            var products = !string.IsNullOrWhiteSpace(search)
                ? await _productService.SearchProductsAsync(search)
                : categoryId.HasValue
                    ? await _productService.GetProductsByCategoryAsync(categoryId.Value)
                    : await _productService.GetAllProductsWithCategoryAsync();

            return View(products);
        }

        /// <summary>Privacy page — required by the default MVC template.</summary>
        public IActionResult Privacy() => View();

        /// <summary>Generic error page — displayed by the exception handler middleware.</summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View();
    }
}