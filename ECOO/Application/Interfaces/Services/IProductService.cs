using ECOO.Application.ViewModels;

namespace ECOO.Application.Interfaces.Services;

/// <summary>
/// Business-logic contract for product operations.
/// The presentation layer only ever depends on this interface,
/// never on repositories or EF Core directly.
/// </summary>
public interface IProductService
{
    Task<IEnumerable<ProductViewModel>> GetAllProductsAsync();
    Task<IEnumerable<ProductViewModel>> GetAllProductsWithCategoryAsync();
    Task<ProductViewModel?> GetProductByIdAsync(int id);
    Task<ProductViewModel?> GetProductByIdWithCategoryAsync(int id);
    Task<IEnumerable<ProductViewModel>> GetProductsByCategoryAsync(int categoryId);
    Task<IEnumerable<ProductViewModel>> SearchProductsAsync(string query);
    Task CreateProductAsync(ProductViewModel model);
    Task UpdateProductAsync(ProductViewModel model);
    Task DeleteProductAsync(int id);
}
