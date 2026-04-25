using ECOO.Application.ViewModels;

namespace ECOO.Application.Interfaces.Services;

/// <summary>
/// Business-logic contract for category operations.
/// </summary>
public interface ICategoryService
{
    Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
    Task<IEnumerable<CategoryViewModel>> GetAllCategoriesOrderedAsync();
    Task<CategoryViewModel?> GetCategoryByIdAsync(int id);
    Task CreateCategoryAsync(CategoryViewModel model);
    Task UpdateCategoryAsync(CategoryViewModel model);
    Task DeleteCategoryAsync(int id);
}
