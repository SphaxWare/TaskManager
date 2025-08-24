using Xunit;
using TaskManager.Domain.Entities;

namespace TaskManager.Tests.DomainTests
{
    public class TaskEntityTests
    {
        [Fact]
        public void Todo_Defaults_Should_Be_Correct()
        {
            // Arrange
            var todo = new Todo();
            todo.Title = "Test Task";

            // Assert
            Assert.Equal("Test Task", todo.Title);
            Assert.False(todo.IsCompleted);
        }
    }
}
