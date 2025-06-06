using TaskManagement.Core.Entities;
using Xunit;

namespace TaskManagement.Tests;

public class ProjectTests
{
    [Fact]
    public void CanBeDeleted_WithNoTasks_ReturnsTrue()
    {
        // Arrange
        var project = new Project();

        // Act
        var result = project.CanBeDeleted();

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void CanBeDeleted_WithPendingTasks_ReturnsFalse()
    {
        // Arrange
        var project = new Project();
        project.Tasks.Add(new Task { Status = TaskStatus.Pending });

        // Act
        var result = project.CanBeDeleted();

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void CanBeDeleted_WithCompletedTasks_ReturnsTrue()
    {
        // Arrange
        var project = new Project();
        project.Tasks.Add(new Task { Status = TaskStatus.Completed });

        // Act
        var result = project.CanBeDeleted();

        // Assert
        Assert.True(result);
    }
} 