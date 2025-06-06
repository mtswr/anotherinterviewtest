using TaskManagement.Core.Entities;

namespace TaskManagement.Core.Interfaces;

public interface IProjectRepository
{
    Task<IEnumerable<Project>> GetAllAsync(Guid userId);
    Task<Project?> GetByIdAsync(Guid id);
    Task<Project> AddAsync(Project project);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> UpdateAsync(Project project);
    Task<int> GetTaskCountAsync(Guid projectId);
} 