using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Core.Entities;

public class ProjectTask
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    
    [MaxLength(2000)]
    public string? Description { get; set; }
    
    public TaskStatus Status { get; set; }
    public TaskPriority Priority { get; set; }
    
    public Guid ProjectId { get; set; }
    public Project Project { get; set; } = null!;
    
    public Guid? AssignedToId { get; set; }
    public User? AssignedTo { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<TaskComment> Comments { get; set; } = new List<TaskComment>();
    public ICollection<TaskHistory> History { get; set; } = new List<TaskHistory>();
}

public enum TaskStatus
{
    Pending,
    InProgress,
    Completed
}

public enum TaskPriority
{
    Low,
    Medium,
    High
} 