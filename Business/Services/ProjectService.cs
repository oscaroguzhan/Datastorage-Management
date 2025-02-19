using System;
using System.Diagnostics;
using System.Linq.Expressions;
using Business.Dtos;
using Business.Factories;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Data.Interfaces;

namespace Business.Services;

public class ProjectService(IProjectRepository projectRepository) : IProjectService
{

    private readonly IProjectRepository _projectRepository = projectRepository;


    public async Task<Project> CreateProjectAsync(ProjectRegistrationForm form)
    {
        // check if project already exists
        var entity = await _projectRepository.GetAsync(p => p.Title == form.Title);

        if (entity!= null)
        {
            Debug.WriteLine("Project already exists");
        }

        // create new project
        var newEntity =await _projectRepository.CreateAsync(ProjectFactory.Create(form));

        return ProjectFactory.Create(newEntity);
        
    }


    public async Task<IEnumerable<Project>> GetAllAsync()
    {
        var entities = await _projectRepository.GetAllAsync();
        return entities.Select(ProjectFactory.Create) ?? [];
    }

    public async Task<Project> GetProjectAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        var entity = await _projectRepository.GetAsync(expression);
        if(entity!=null)
        {
            return ProjectFactory.Create(entity);

        }
        return null!;
    }

    
    public async Task<Project> UpdateProjectAsync(ProjectUpdateForm form)
    {
        var entity = await _projectRepository.GetAsync(p => p.Id == form.Id);
        if(entity!=null)
        {
            entity.Title = form.Title;
            entity.Description = form.Description;
            entity.StartDate = form.StartDate;
            entity.EndDate = form.EndDate;

            await _projectRepository.UpdateAsync(e => e.Id == form.Id, entity);
            return ProjectFactory.Create(entity);
        }
        return null!;
 
    }
    
    public async Task<bool> DeleteProjectAsync(int id)
    {
        var entity = await _projectRepository.DeleteAsync( e => e.Id == id);
        
        return entity!;
    }


    public async Task<bool> CheckIfProjectExistsAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        return await _projectRepository.AlreadyExistsAsync(expression);
    }
}
