using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos.Customer;

public class CustomerRegistrationForm
{
    [Required]
    public string CustomerName { get; set; } = null!;
}
