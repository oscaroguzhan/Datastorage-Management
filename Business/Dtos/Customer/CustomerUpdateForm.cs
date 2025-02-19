using System;

namespace Business.Dtos.Customer;

public class CustomerUpdateForm
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
}
