namespace TaskManagement.Core.Entities;

public class TaskHistory : BaseEntity
{
    public Guid TaskId { get; set; }
    public ProjectTask Task { get; set; } = null!;
    public Guid UserId { get; set; }
    public string ChangeType { get; set; } = string.Empty;
    public string OldValue { get; set; } = string.Empty;
    public string NewValue { get; set; } = string.Empty;
} 