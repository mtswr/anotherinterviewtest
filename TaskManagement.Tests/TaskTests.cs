using TaskManagement.Core.Entities;
using Xunit;

namespace TaskManagement.Tests;

public class TaskTests
{
    [Fact]
    public void CreateTask_WithValidData_SetsPropertiesCorrectly()
    {
        // Arrange
        var title = "Test Task";
        var description = "Test Description";
        var dueDate = DateTime.UtcNow.AddDays(7);
        var priority = TaskPriority.High;

        // Act
        var task = new Task
        {
            Title = title,
            Description = description,
            DueDate = dueDate,
            Priority = priority,
            Status = TaskStatus.Pending
        };

        // Assert
        Assert.Equal(title, task.Title);
        Assert.Equal(description, task.Description);
        Assert.Equal(dueDate, task.DueDate);
        Assert.Equal(priority, task.Priority);
        Assert.Equal(TaskStatus.Pending, task.Status);
    }

    [Fact]
    public void AddComment_ToTask_AddsToCommentsCollection()
    {
        // Arrange
        var task = new Task();
        var comment = new TaskComment
        {
            Content = "Test Comment",
            UserId = Guid.NewGuid()
        };

        // Act
        task.Comments.Add(comment);

        // Assert
        Assert.Single(task.Comments);
        Assert.Equal(comment, task.Comments.First());
    }

    [Fact]
    public void AddHistory_ToTask_AddsToHistoryCollection()
    {
        // Arrange
        var task = new Task();
        var history = new TaskHistory
        {
            ChangeType = "Status",
            OldValue = "Pending",
            NewValue = "InProgress",
            UserId = Guid.NewGuid()
        };

        // Act
        task.History.Add(history);

        // Assert
        Assert.Single(task.History);
        Assert.Equal(history, task.History.First());
    }
} 