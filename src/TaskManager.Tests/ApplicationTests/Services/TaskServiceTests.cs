using Xunit;
using Moq;
using TaskManager.Application.Services;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace TaskManager.Tests.ApplicationTests.Services
{
    public class TaskServiceTests
    {
        [Fact]
        public async Task AddTask_Should_Call_Repository()
        {
            // Arrange
            var repoMock = new Mock<ITaskRepository>();
            var service = new TaskService(repoMock.Object);
            var todo = new Todo { Title = "Test Task" };

            // Act
            await service.AddTaskAsync(todo);

            // Assert
            repoMock.Verify(r => r.AddTaskAsync(It.Is<Todo>(t => t.Title == "Test Task")), Times.Once);
        }

        [Fact]
        public async Task GetAllTasks_Should_Return_All_Todos()
        {
            // Arrange
            var todos = new List<Todo>
            {
                new Todo { Title = "Task 1" },
                new Todo { Title = "Task 2" }
            };
            var repoMock = new Mock<ITaskRepository>();
            repoMock.Setup(r => r.GetAllTasksAsync()).ReturnsAsync(todos);

            var service = new TaskService(repoMock.Object);

            // Act
            var result = await service.GetAllTasksAsync();

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Contains(result, t => t.Title == "Task 1");
        }

        [Fact]
        public async Task GetTaskById_Should_Return_Correct_Task()
        {
            // Arrange
            var todoId = Guid.NewGuid();
            var todo = new Todo { Id = todoId, Title = "Task 1" };
            var repoMock = new Mock<ITaskRepository>();
            repoMock.Setup(r => r.GetTaskByIdAsync(todoId)).ReturnsAsync(todo);

            var service = new TaskService(repoMock.Object);

            // Act
            var result = await service.GetTaskByIdAsync(todoId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(todoId, result!.Id);
            Assert.Equal("Task 1", result.Title);
        }

        [Fact]
        public async Task UpdateTask_Should_Call_Repository()
        {
            // Arrange
            var repoMock = new Mock<ITaskRepository>();
            var service = new TaskService(repoMock.Object);
            var todo = new Todo { Title = "Updated Task" };

            // Act
            await service.UpdateTaskAsync(todo);

            // Assert
            repoMock.Verify(r => r.UpdateTaskAsync(It.Is<Todo>(t => t.Title == "Updated Task")), Times.Once);
        }

        [Fact]
        public async Task DeleteTask_Should_Call_Repository()
        {
            // Arrange
            var repoMock = new Mock<ITaskRepository>();
            var service = new TaskService(repoMock.Object);
            var id = Guid.NewGuid();

            // Act
            await service.DeleteTaskAsync(id);

            // Assert
            repoMock.Verify(r => r.DeleteTaskAsync(id), Times.Once);
        }
    }
}