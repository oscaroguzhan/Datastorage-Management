// Purpose: Contains the ProjectsController which is responsible for handling requests to the /api/projects endpoint.
using Business.Dtos;
using Business.Interfaces;
using Business.Models;

using Microsoft.AspNetCore.Mvc;

namespace PresentationWebAPI.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController(IProjectService projectService) : ControllerBase
    {
        private readonly IProjectService _projectService = projectService;

        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetAll()
        {
            return Ok(_projectService.GetAllAsync());    

        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectRegistrationForm form)
        {
            if(ModelState.IsValid) 
            {
                var exists = await _projectService.CheckIfProjectExistsAsync( p => p.Title == form.Title);
                if(exists)
                {
                    return Conflict("Project already exists");
                }
                var project = await _projectService.CreateProjectAsync(form);
                if( project != null)
                    return Ok(project);
            }
            return BadRequest();
        }
    }
}
