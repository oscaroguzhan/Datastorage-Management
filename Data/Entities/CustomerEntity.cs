
using System.ComponentModel.DataAnnotations;

namespace Data.Entities;

public class CustomerEntity
{
    [Key]
    public Guid Id { get; set; }
    public string CustomerName { get; set; } = null!;
}

