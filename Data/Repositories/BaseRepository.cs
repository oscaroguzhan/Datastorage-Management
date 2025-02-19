
using System.Diagnostics;
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
    protected readonly DataContext _context = context;
    protected readonly DbSet<TEntity> _db = context.Set<TEntity>();

    public virtual async Task<TEntity> CreateAsync(TEntity entity)
    {
        if(entity == null) {
            return null!;
        }
        try
        {
            await _db.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex) 
        {
            
            Debug.WriteLine($"Error creating {nameof(TEntity)}: {ex.Message}");
            return null!;
        }
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        var entities = await _db.ToListAsync();
        return entities;
    }

    public virtual async Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        if(expression == null)
        {
            return null!;
        }
        return await _db.FirstOrDefaultAsync(expression) ?? null!;
        
    }

    public virtual async Task<TEntity> UpdateAsync(Expression<Func<TEntity, bool>> expression, TEntity updateteEntity)
        {
            if(updateteEntity == null)
            {
                return null!;
            }
            try
            {
                var existingEntity = await _db.FirstOrDefaultAsync(expression) ?? null!;
                if(existingEntity == null) 
                {
                    return null!;
                }
                _context.Entry(existingEntity).CurrentValues.SetValues(updateteEntity);
                await _context.SaveChangesAsync();
                return existingEntity;
            }
            catch (Exception ex) 
            {
            
            Debug.WriteLine($"Error updating {nameof(TEntity)}: {ex.Message}");
            return null!;
            }
        }
    public virtual async Task<bool> DeleteAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await _db.FirstOrDefaultAsync(expression );
            if(entity== null)
            {
                return false;  
            }
            _db.Remove(entity);
            await _context.SaveChangesAsync();
            return true;  
        }
        catch (Exception ex) 
        { 
            Debug.WriteLine($"Error deleting {nameof(TEntity)}: {ex.Message}");
            return false;
        }
    }

    public virtual async Task<bool> AlreadyExistsAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _db.AllAsync(expression);
    }
}
