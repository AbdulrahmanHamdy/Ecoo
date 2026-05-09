using ECOO.Application.Interfaces.Repositories;
using ECOO.Application.Interfaces.Services;
using ECOO.Application.ViewModels;
using ECOO.Domain.Entities;

namespace ECOO.Application.Services;


public class ProductService : IProductService
{
    private readonly IProductRepository _productRepo;

    public ProductService(IProductRepository productRepo)
    {
        _productRepo = productRepo;
    }

    

    public async Task<IEnumerable<ProductViewModel>> GetAllProductsAsync()
    {
        var products = await _productRepo.GetAllAsync();
        return products.Select(MapToViewModel);
    }

    public async Task<IEnumerable<ProductViewModel>> GetAllProductsWithCategoryAsync()
    {
        var products = await _productRepo.GetAllWithCategoryAsync();
        return products.Select(MapToViewModel);
    }

    public async Task<ProductViewModel?> GetProductByIdAsync(int id)
    {
        var product = await _productRepo.GetByIdAsync(id);
        return product is null ? null : MapToViewModel(product);
    }

    public async Task<ProductViewModel?> GetProductByIdWithCategoryAsync(int id)
    {
        var product = await _productRepo.GetByIdWithCategoryAsync(id);
        return product is null ? null : MapToViewModel(product);
    }

    public async Task<IEnumerable<ProductViewModel>> GetProductsByCategoryAsync(int categoryId)
    {
        var products = await _productRepo.GetByCategoryAsync(categoryId);
        return products.Select(MapToViewModel);
    }

    public async Task<IEnumerable<ProductViewModel>> SearchProductsAsync(string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return await GetAllProductsWithCategoryAsync();

        var products = await _productRepo.SearchAsync(query);
        return products.Select(MapToViewModel);
    }

   
    public async Task CreateProductAsync(ProductViewModel model)
    {
        var entity = MapToEntity(model);
        await _productRepo.AddAsync(entity);
    }

    public async Task UpdateProductAsync(ProductViewModel model)
    {
        var entity = MapToEntity(model);
        await _productRepo.UpdateAsync(entity);
    }

    public async Task DeleteProductAsync(int id)
        => await _productRepo.DeleteAsync(id);

   
    private static ProductViewModel MapToViewModel(Product p) => new()
    {
        Id           = p.Id,
        Name         = p.Name,
        Description  = p.Description,
        Price        = p.Price,
        ImageUrl     = p.ImageUrl,
        CategoryId   = p.CategoryId,
        CategoryName = p.Category?.Name
    };

   
    private static Product MapToEntity(ProductViewModel vm) => new()
    {
        Id          = vm.Id,
        Name        = vm.Name,
        Description = vm.Description,
        Price       = vm.Price,
        ImageUrl    = vm.ImageUrl,
        CategoryId  = vm.CategoryId
    };
}
