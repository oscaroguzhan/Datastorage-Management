using System;
using System.Linq.Expressions;
using Business.Dtos;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{

    private readonly IProjectRepository _projectRepository = projectRepository;


    public Task<Project> CreateProjectAsync(ProjectRegistrationForm form)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Project>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }

    
    public Task<Project> UpdateProjectAsync(ProjectUpdateForm form)
    {
        throw new NotImplementedException();
    }
    
    public Task<bool> DeleteProjectAsync(int id)
    {
        throw new NotImplementedException();
    }


    public Task<bool> CheckIfProjectExistsAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        throw new NotImplementedException();
    }
}
