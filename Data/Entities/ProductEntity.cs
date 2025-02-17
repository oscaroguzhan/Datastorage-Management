using System;

namespace Data.Entities;

public class ProductEntity
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = null!;
    public decimal Price { get; set; }
}
