
namespace Business.Models;

public class Customer
{
    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = [];



    
}
