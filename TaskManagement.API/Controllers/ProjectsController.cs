using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.DTOs;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectRepository _projectRepository;

    public ProjectsController(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Project>>> GetProjects([FromQuery] Guid userId)
    {
        var projects = await _projectRepository.GetAllAsync(userId);
        return Ok(projects);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Project>> GetProject(Guid id)
    {
        var project = await _projectRepository.GetByIdAsync(id);
        if (project == null)
            return NotFound();

        return Ok(project);
    }

    [HttpPost]
    public async Task<ActionResult<Project>> CreateProject(CreateProjectDto projectDto)
    {
        var project = new Project
        {
            Name = projectDto.Name,
            Description = projectDto.Description,
            OwnerId = projectDto.OwnerId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        var createdProject = await _projectRepository.AddAsync(project);
        return CreatedAtAction(nameof(GetProject), new { id = createdProject.Id }, createdProject);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProject(Guid id, Project project)
    {
        if (id != project.Id)
            return BadRequest();

        var success = await _projectRepository.UpdateAsync(project);
        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(Guid id)
    {
        var success = await _projectRepository.DeleteAsync(id);
        if (!success)
            return BadRequest("Cannot delete project with pending tasks");

        return NoContent();
    }
} 