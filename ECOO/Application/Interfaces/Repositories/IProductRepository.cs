using ECOO.Domain.Entities;

namespace ECOO.Application.Interfaces.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    
    Task<IEnumerable<Product>> GetAllWithCategoryAsync();

  
    Task<Product?> GetByIdWithCategoryAsync(int id);

    
    Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId);

    Task<IEnumerable<Product>> SearchAsync(string query);
}
