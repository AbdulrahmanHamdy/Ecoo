using ECOO.Domain.Entities;

namespace ECOO.Application.Interfaces.Repositories;

/// <summary>
/// Product-specific repository contract.
/// Extends the generic repository with queries that need
/// eager-loading or filtering specific to products.
/// </summary>
public interface IProductRepository : IGenericRepository<Product>
{
    /// <summary>Returns all products with their Category navigation property loaded.</summary>
    Task<IEnumerable<Product>> GetAllWithCategoryAsync();

    /// <summary>Returns a single product with its Category navigation property, or null.</summary>
    Task<Product?> GetByIdWithCategoryAsync(int id);

    /// <summary>Returns all products belonging to a specific category.</summary>
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);

    /// <summary>Simple text search across product names and descriptions.</summary>
    Task<IEnumerable<Product>> SearchAsync(string query);
}
