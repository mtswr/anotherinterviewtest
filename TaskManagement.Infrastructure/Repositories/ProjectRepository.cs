using Microsoft.EntityFrameworkCore;
using TaskManagement.Core.Entities;
using TaskManagement.Core.Interfaces;
using TaskManagement.Infrastructure.Data;

namespace TaskManagement.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly ApplicationDbContext _context;

    public ProjectRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Project>> GetAllAsync(Guid userId)
    {
        return await _context.Projects
            .Include(p => p.Owner)
            .Include(p => p.Tasks)
            .Where(p => p.OwnerId == userId)
            .ToListAsync();
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _context.Projects
            .Include(p => p.Owner)
            .Include(p => p.Tasks)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Project>> GetByOwnerIdAsync(Guid ownerId)
    {
        return await _context.Projects
            .Include(p => p.Owner)
            .Include(p => p.Tasks)
            .Where(p => p.OwnerId == ownerId)
            .ToListAsync();
    }

    public async Task<Project> AddAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    public async Task<bool> UpdateAsync(Project project)
    {
        try
        {
            _context.Projects.Update(project);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var project = await _context.Projects.FindAsync(id);
            if (project != null)
            {
                _context.Projects.Remove(project);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public async Task<int> GetTaskCountAsync(Guid projectId)
    {
        return await _context.Tasks
            .CountAsync(t => t.ProjectId == projectId);
    }
} 