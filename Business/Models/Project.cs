using System;
using Data.Entities;

namespace Business.Models;

public class Project
{
    public int Id { get; set; }

    public Customer CustomerName { get; set; } = null!;

    public StatusTypeEntity StatusType { get; set; } = null!;

    
    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime StartDate { get; set; } 
    public DateTime? EndDate { get; set; }

    
    
    
}
