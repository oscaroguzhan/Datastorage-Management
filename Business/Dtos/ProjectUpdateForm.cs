using System;
using System.ComponentModel.DataAnnotations;

namespace Business.Dtos;

public class ProjectUpdateForm
{
    [Required]
    public int Id { get; set; }
    public string Title { get; set; }  = null!;
    public string? Description { get; set; }
    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int StatusTypeId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
