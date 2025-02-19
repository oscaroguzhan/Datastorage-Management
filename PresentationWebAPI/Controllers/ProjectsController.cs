// Purpose: Contains the ProjectsController which is responsible for handling requests to the /api/projects endpoint.
using Business.Dtos;
using Business.Interfaces;
using Business.Models;

using Microsoft.AspNetCore.Mvc;

namespace PresentationWebAPI.Controllers
{
    [Route("api/projects")]
    [ApiController]
    public class ProjectsController: ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectsController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProjectRegistrationForm form)
        {
            if(!ModelState.IsValid) 
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                return BadRequest(new { Errors = errors });
            }
            var exists = await _projectService.CheckIfProjectExistsAsync(p => p.Title == form.Title);
            if (exists)
            {
                return Conflict("Project already exists");
            }

            var project = await _projectService.CreateProjectAsync(form);
            if (project != null)
                return Ok(project);

            return BadRequest();
        }

        
        [HttpGet]
        public ActionResult<IEnumerable<Project>> GetAll()
        {
            return Ok(_projectService.GetAllAsync());    

        }

    }
}
