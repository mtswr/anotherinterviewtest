using System.ComponentModel.DataAnnotations;

namespace TaskManagement.Core.Entities;

public class User
{
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(50)]
    public string Username { get; set; } = string.Empty;
    
    [Required]
    [MaxLength(100)]
    public string Email { get; set; } = string.Empty;
    
    [Required]
    public string PasswordHash { get; set; } = string.Empty;
    
    [Required]
    public UserRole Role { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public ICollection<Project> OwnedProjects { get; set; } = new List<Project>();
    public ICollection<ProjectTask> AssignedTasks { get; set; } = new List<ProjectTask>();
}

public enum UserRole
{
    User,
    Manager,
    Admin
} 