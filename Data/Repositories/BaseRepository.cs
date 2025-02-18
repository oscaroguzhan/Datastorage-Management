
using System.Linq.Expressions;
using Data.Contexts;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;
/// <summary>
/// Create a generic repository so that it can be inherited by other repositories.
/// This will allow us to avoid repeating the same code in each repository.
/// The repository will have a DataContext and a DbSet.
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public abstract class BaseRepository<TEntity>(DataContext context) : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DbContext _context = context;
    protected readonly DbSet<TEntity> _db = context.Set<TEntity>();

    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        await _db.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var entities = await _db.ToListAsync();
        return entities;
    }
    public Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _db.FirstOrDefaultAsync(expression);
        return entity;
    }

    public async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        var entity = await _db.FirstOrDefaultAsync(expression);
        if (entity != null)
        {
            _db.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }



    public async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression)
    {
        var existingEntity = await _db.FirstOrDefaultAsync(expression);
        if (existingEntity != null)
        {
            _db.Update(existingEntity);
            await _context.SaveChangesAsync();
            return existingEntity;

        }
        return null!;
    }

}
