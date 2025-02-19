using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Business.Dtos.Customer;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class CustomerService(ICustomerRepository customerRepository) : ICustomerService 
{
    private readonly ICustomerRepository _customerRepository = customerRepository;

    public async Task<Customer> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var entity = await _customerRepository.GetAsync(c => c.CustomerName == form.CustomerName);

        if (entity!= null)
        {
           Debug.WriteLine("Customer already exists");
        }

        var newEntity = await _customerRepository.CreateAsync(CustomerFactory.Create(form));

        return CustomerFactory.Create(newEntity);
    }



    public async Task<bool> CheckIfCustomerExistsAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        return await _customerRepository.AlreadyExistsAsync(expression);
    }

    public Task<bool> DeleteCustomerAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var entities = await _customerRepository.GetAllAsync();
        return entities.Select(CustomerFactory.Create) ?? [];
    }


    public Task<Customer> GetCustomerAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    public Task<Customer> UpdateCustomerAsync(CustomerUpdateForm form)
    {
        throw new NotImplementedException();
    }
}
