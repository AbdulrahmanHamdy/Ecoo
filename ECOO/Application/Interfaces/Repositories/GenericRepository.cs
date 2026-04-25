using ECOO.Application.Interfaces.Repositories;
using ECOO.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ECOO.Infrastructure.Repositories;

/// <summary>
/// Concrete generic repository backed by Entity Framework Core.
/// All operations use async/await to avoid blocking the thread pool.
/// </summary>
/// <typeparam name="T">Domain entity type.</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext context)
    {
        _context = context;
        _dbSet   = context.Set<T>();
    }

    /// <inheritdoc/>
    public virtual async Task<IEnumerable<T>> GetAllAsync()
        => await _dbSet.AsNoTracking().ToListAsync();

    /// <inheritdoc/>
    public virtual async Task<T?> GetByIdAsync(int id)
        => await _dbSet.FindAsync(id);

    /// <inheritdoc/>
    public virtual async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public virtual async Task UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity is not null)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
