using System;
using Business.Dtos.Customer;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class CustomerFactory
{
    public static CustomerRegistrationForm Create() => new();

    public static CustomerEntity Create(CustomerRegistrationForm form) => new()
    {
        CustomerName = form.CustomerName
    };

    public static Customer Create(CustomerEntity entity) => new()
    {
        Id = entity.Id,
        CustomerName = entity.CustomerName
    };

    public static CustomerEntity Create(CustomerUpdateForm form) => new()
    {
        Id = form.Id,
        CustomerName = form.CustomerName
    };

    public static CustomerUpdateForm Update(Customer customer) => new()
    {
        Id = customer.Id,
        CustomerName = customer.CustomerName
    };
}
