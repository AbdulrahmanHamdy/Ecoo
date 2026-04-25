using ECOO.Application.Interfaces.Repositories;
using ECOO.Application.Interfaces.Services;
using ECOO.Application.ViewModels;
using ECOO.Domain.Entities;

namespace ECOO.Application.Services;

/// <summary>
/// Handles all category-related business logic and entity↔ViewModel mapping.
/// </summary>
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepo;

    public CategoryService(ICategoryRepository categoryRepo)
    {
        _categoryRepo = categoryRepo;
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
    {
        var cats = await _categoryRepo.GetAllAsync();
        return cats.Select(MapToViewModel);
    }

    public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesOrderedAsync()
    {
        var cats = await _categoryRepo.GetAllOrderedAsync();
        return cats.Select(MapToViewModel);
    }

    public async Task<CategoryViewModel?> GetCategoryByIdAsync(int id)
    {
        var cat = await _categoryRepo.GetByIdAsync(id);
        return cat is null ? null : MapToViewModel(cat);
    }

    public async Task CreateCategoryAsync(CategoryViewModel model)
        => await _categoryRepo.AddAsync(MapToEntity(model));

    public async Task UpdateCategoryAsync(CategoryViewModel model)
        => await _categoryRepo.UpdateAsync(MapToEntity(model));

    public async Task DeleteCategoryAsync(int id)
        => await _categoryRepo.DeleteAsync(id);

    // ── Mapping helpers ────────────────────────────────────────────

    private static CategoryViewModel MapToViewModel(Category c) => new()
    {
        Id   = c.Id,
        Name = c.Name
    };

    private static Category MapToEntity(CategoryViewModel vm) => new()
    {
        Id   = vm.Id,
        Name = vm.Name
    };
}
