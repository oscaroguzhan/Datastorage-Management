
using System.Linq.Expressions;
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class ProjectRepository(DataContext context) : BaseRepository<ProjectEntity>(context), IProjectRepository
{
    private readonly DataContext _context = context;

    public override async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        var entities = await _context.Projects
        .Include(p => p.Product)
        .ToListAsync();
        return entities;
    }

    public override async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
       if(expression == null) 
       {
        return null!;
       }
       return await _context.Projects
            .Include(u => u.Customer)
            .FirstOrDefaultAsync(expression) ?? null!;
    }
}
