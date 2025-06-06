using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Core.Entities;

public class Project : BaseEntity
{
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    public Guid OwnerId { get; set; }
    public User Owner { get; set; } = null!;
    
    public ICollection<ProjectTask> Tasks { get; set; } = new List<ProjectTask>();

    public int GetCompletedTasksCount()
    {
        return Tasks.Count(t => t.Status == TaskManagement.Core.Entities.TaskStatus.Completed);
    }

    public int GetInProgressTasksCount()
    {
        return Tasks.Count(t => t.Status == TaskManagement.Core.Entities.TaskStatus.InProgress);
    }

    public int GetPendingTasksCount()
    {
        return Tasks.Count(t => t.Status == TaskManagement.Core.Entities.TaskStatus.Pending);
    }

    public bool CanBeDeleted()
    {
        return !Tasks.Any(t => t.Status == TaskManagement.Core.Entities.TaskStatus.Pending);
    }
} 