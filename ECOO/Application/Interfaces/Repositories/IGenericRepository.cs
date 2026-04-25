namespace ECOO.Application.Interfaces.Repositories;

/// <summary>
/// Generic repository contract that provides standard CRUD operations
/// for any entity type T. Keeps infrastructure concerns out of the
/// Application and Domain layers.
/// </summary>
/// <typeparam name="T">Domain entity type.</typeparam>
public interface IGenericRepository<T> where T : class
{
    /// <summary>Returns all entities (no tracking).</summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>Returns a single entity by primary key, or null.</summary>
    Task<T?> GetByIdAsync(int id);

    /// <summary>Adds a new entity and persists it.</summary>
    Task AddAsync(T entity);

    /// <summary>Updates an existing entity and persists changes.</summary>
    Task UpdateAsync(T entity);

    /// <summary>Removes an entity by primary key and persists the deletion.</summary>
    Task DeleteAsync(int id);
}
