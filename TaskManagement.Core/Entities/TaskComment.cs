namespace TaskManagement.Core.Entities;

public class TaskComment : BaseEntity
{
    public string Content { get; set; } = string.Empty;
    public Guid TaskId { get; set; }
    public ProjectTask Task { get; set; } = null!;
    public Guid UserId { get; set; }
} 