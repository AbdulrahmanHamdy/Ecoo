using ECOO.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace ECOO.Presentation.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public HomeController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        
        public async Task<IActionResult> Index(int? categoryId, string? search)
        {
            
            var categories = await _categoryService.GetAllCategoriesOrderedAsync();
            ViewBag.Categories = categories;
            ViewBag.SelectedCategoryId = categoryId;
            ViewBag.Search = search;

          
            var products = !string.IsNullOrWhiteSpace(search)
                ? await _productService.SearchProductsAsync(search)
                : categoryId.HasValue
                    ? await _productService.GetProductsByCategoryAsync(categoryId.Value)
                    : await _productService.GetAllProductsWithCategoryAsync();

            return View(products);
        }

       
        public IActionResult Privacy() => View();

       
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View();
    }
}