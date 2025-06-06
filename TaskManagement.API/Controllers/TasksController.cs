using Microsoft.AspNetCore.Mvc;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;

namespace TaskManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    private readonly IProjectRepository _projectRepository;

    public TasksController(ITaskRepository taskRepository, IProjectRepository projectRepository)
    {
        _taskRepository = taskRepository;
        _projectRepository = projectRepository;
    }

    [HttpGet("project/{projectId}")]
    public async Task<ActionResult<IEnumerable<ProjectTask>>> GetProjectTasks(Guid projectId)
    {
        var tasks = await _taskRepository.GetTasksByProjectAsync(projectId);
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProjectTask>> GetTask(Guid id)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            return NotFound();

        return Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<ProjectTask>> CreateTask(ProjectTask task)
    {
        var taskCount = await _projectRepository.GetTaskCountAsync(task.ProjectId);
        if (taskCount >= 20)
            return BadRequest("Project has reached maximum task limit (20)");

        var createdTask = await _taskRepository.AddAsync(task);
        return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(Guid id, ProjectTask task)
    {
        if (id != task.Id)
            return BadRequest();

        var existingTask = await _taskRepository.GetByIdAsync(id);
        if (existingTask == null)
            return NotFound();

        if (existingTask.Priority != task.Priority)
            return BadRequest("Task priority cannot be changed after creation");

        var updatedTask = await _taskRepository.UpdateAsync(task);
        if (updatedTask == null)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(Guid id)
    {
        await _taskRepository.DeleteAsync(id);
        return NoContent();
    }

    [HttpPost("{id}/comments")]
    public async Task<ActionResult<TaskComment>> AddComment(Guid id, TaskComment comment)
    {
        var task = await _taskRepository.GetByIdAsync(id);
        if (task == null)
            return NotFound();

        comment.TaskId = id;
        await _taskRepository.AddCommentAsync(comment);
        return Ok(comment);
    }

    [HttpGet("{id}/comments")]
    public async Task<ActionResult<IEnumerable<TaskComment>>> GetTaskComments(Guid id)
    {
        var comments = await _taskRepository.GetTaskCommentsAsync(id);
        return Ok(comments);
    }

    [HttpGet("{id}/history")]
    public async Task<ActionResult<IEnumerable<TaskHistory>>> GetTaskHistory(Guid id)
    {
        var history = await _taskRepository.GetTaskHistoryAsync(id);
        return Ok(history);
    }
} 