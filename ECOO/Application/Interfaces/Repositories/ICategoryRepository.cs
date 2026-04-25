using ECOO.Domain.Entities;

namespace ECOO.Application.Interfaces.Repositories;

/// <summary>
/// Category-specific repository contract.
/// Inherits all generic CRUD operations and adds any
/// category-specific queries.
/// </summary>
public interface ICategoryRepository : IGenericRepository<Category>
{
    /// <summary>
    /// Returns all categories ordered alphabetically.
    /// Useful for populating drop-downs throughout the UI.
    /// </summary>
    Task<IEnumerable<Category>> GetAllOrderedAsync();
}
