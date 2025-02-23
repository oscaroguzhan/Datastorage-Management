using System;
using Business.Dtos;
using Business.Models;
using Data.Entities;

namespace Business.Factories;

public static class ProjectFactory
{
    public static ProjectRegistrationForm Create() => new();

    public static ProjectEntity Create(ProjectRegistrationForm form)  => new()
    {
            Title = form.Title,
            Description = form.Description,
            StartDate = form.StartDate,

            // fetch customer and status type details
            CustomerId = form.CustomerId,
            StatusTypeId = form.StatusTypeId,
            EndDate = form.EndDate
    };

    public static Project Create(ProjectEntity entity) 
    {
        // check if the entity is null
        if (entity != null)
        {
            return new Project
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                CustomerName = new Customer
                {
                    Id = entity.CustomerId,
                    CustomerName = entity.Customer.CustomerName

                },
                StartDate = entity.StartDate,
                StatusType = new StatusTypeEntity
                {
                    Id = entity.StatusTypeId,
                    StatusName = entity.StatusType.StatusName,  
                },
                EndDate = entity.EndDate
            };  
        }
        return null!;
    }

    public static ProjectEntity Create(ProjectUpdateForm form) => new()
    {
        Id = form.Id,
        Title = form.Title,
        Description = form.Description,
        StartDate = form.StartDate,
        EndDate = form.EndDate
    };

    public static ProjectUpdateForm Update(Project project) => new()
    {
        Id = project.Id,
        Title = project.Title,
        Description = project.Description,
        StartDate = project.StartDate,
        EndDate = project.EndDate       
    };
}
