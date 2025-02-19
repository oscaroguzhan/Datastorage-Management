using System;
using Data.Entities;

namespace Data.Interfaces;

public interface ICustomerRepository : IBaseRepository<CustomerEntity>
{
    // get all customers with the products
    //Task<IEnumerable<CustomerEntity>> GetAllWithProductAsync();
}
