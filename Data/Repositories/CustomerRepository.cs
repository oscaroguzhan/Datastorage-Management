
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
{
    //private readonly DataContext _context;

   //public async Task<IEnumerable<CustomerEntity>> GetAllWithProductAsync()
   // {
    //     return await _context.Customers
//            .Include(c => c.Products)
//            .ToListAsync();
//    }
}
