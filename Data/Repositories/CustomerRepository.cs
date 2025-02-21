
using Data.Contexts;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class CustomerRepository(DataContext context) : BaseRepository<CustomerEntity>(context), ICustomerRepository
{
    

   
}
