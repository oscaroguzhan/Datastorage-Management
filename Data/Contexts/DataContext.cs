
using Data.Entities;
using Microsoft.EntityFrameworkCore;
namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{

    public virtual DbSet<CustomerEntity> Customers { get; set; }
    
    public virtual DbSet<ProductEntity> Products { get; set; }
    public virtual DbSet<StatusTypeEntity> StatusTypes { get; set; }
    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<ProjectEntity> Projects { get; set; }

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseLazyLoadingProxies();
    }
    
}



