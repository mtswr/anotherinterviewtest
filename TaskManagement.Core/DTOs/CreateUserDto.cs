using System.ComponentModel.DataAnnotations;
using TaskManagement.Core.Entities;

namespace TaskManagement.Core.DTOs;

public class CreateUserDto
{
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
} 