
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class ProjectEntity 
{
    [Key]
    public int Id { get; set; }
    public string Title { get; set; } = null!;

    public string? Description { get; set; } 

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }
    [Column(TypeName = "date")]
    public DateTime? EndDate { get; set; }

    // RELATIONSHIP TO THE OTHER TABLES
    public int CustomerId { get; set; }
    public virtual CustomerEntity Customer { get; set; } = null!;

    public int StatusTypeId { get; set; }
    public virtual StatusTypeEntity StatusType { get; set; } = null!;

    public int UserId { get; set; }
    public virtual UserEntity User { get; set; } = null!;
    public int ProductId { get; set; }
    public virtual ProductEntity Product { get; set; } = null!;

}