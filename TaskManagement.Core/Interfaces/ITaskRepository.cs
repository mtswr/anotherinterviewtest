using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Interfaces;

public interface ITaskRepository
{
    Task<IEnumerable<ProjectTask>> GetAllAsync();
    Task<ProjectTask?> GetByIdAsync(Guid id);
    Task<ProjectTask> AddAsync(ProjectTask task);
    Task<ProjectTask> UpdateAsync(ProjectTask task);
    Task DeleteAsync(Guid id);
    Task<IEnumerable<ProjectTask>> GetTasksByProjectAsync(Guid projectId);
    Task<IEnumerable<ProjectTask>> GetTasksByUserAsync(Guid userId);
    Task<IEnumerable<ProjectTask>> GetCompletedTasksByUserAsync(Guid userId, DateTime startDate, DateTime endDate);
    Task AddCommentAsync(TaskComment comment);
    Task AddHistoryAsync(TaskHistory history);
    Task<IEnumerable<TaskHistory>> GetTaskHistoryAsync(Guid taskId);
    Task<IEnumerable<TaskComment>> GetTaskCommentsAsync(Guid taskId);
} 