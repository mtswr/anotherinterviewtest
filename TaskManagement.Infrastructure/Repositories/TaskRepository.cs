using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ApplicationDbContext _context;

    public TaskRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectTask>> GetAllAsync()
    {
        return await _context.Tasks
            .Include(t => t.Project)
            .Include(t => t.AssignedTo)
            .ToListAsync();
    }

    public async Task<ProjectTask?> GetByIdAsync(Guid id)
    {
        return await _context.Tasks
            .Include(t => t.Project)
            .Include(t => t.AssignedTo)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<ProjectTask> AddAsync(ProjectTask task)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<ProjectTask> UpdateAsync(ProjectTask task)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync();
        return task;
    }

    public async Task DeleteAsync(Guid id)
    {
        var task = await _context.Tasks.FindAsync(id);
        if (task != null)
        {
            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ProjectTask>> GetTasksByProjectAsync(Guid projectId)
    {
        return await _context.Tasks
            .Include(t => t.Project)
            .Include(t => t.AssignedTo)
            .Where(t => t.ProjectId == projectId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProjectTask>> GetTasksByUserAsync(Guid userId)
    {
        return await _context.Tasks
            .Include(t => t.Project)
            .Include(t => t.AssignedTo)
            .Where(t => t.AssignedToId == userId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProjectTask>> GetCompletedTasksByUserAsync(Guid userId, DateTime startDate, DateTime endDate)
    {
        return await _context.Tasks
            .Include(t => t.Project)
            .Include(t => t.AssignedTo)
            .Where(t => t.AssignedToId == userId && 
                       t.Status == TaskManagement.Core.Entities.TaskStatus.Completed &&
                       t.UpdatedAt >= startDate &&
                       t.UpdatedAt <= endDate)
            .ToListAsync();
    }

    public async Task AddCommentAsync(TaskComment comment)
    {
        comment.CreatedAt = DateTime.UtcNow;
        _context.TaskComments.Add(comment);
        await _context.SaveChangesAsync();
    }

    public async Task AddHistoryAsync(TaskHistory history)
    {
        history.CreatedAt = DateTime.UtcNow;
        _context.TaskHistory.Add(history);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskHistory>> GetTaskHistoryAsync(Guid taskId)
    {
        return await _context.TaskHistory
            .Where(h => h.TaskId == taskId)
            .OrderByDescending(h => h.CreatedAt)
            .ToListAsync();
    }

    public async Task<IEnumerable<TaskComment>> GetTaskCommentsAsync(Guid taskId)
    {
        return await _context.TaskComments
            .Where(c => c.TaskId == taskId)
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }
} 