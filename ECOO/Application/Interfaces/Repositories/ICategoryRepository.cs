using ECOO.Domain.Entities;

namespace ECOO.Application.Interfaces.Repositories;


public interface ICategoryRepository : IGenericRepository<Category>
{
    
    Task<IEnumerable<Category>> GetAllOrderedAsync();
}
