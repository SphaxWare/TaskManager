using TaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskService
    {
        Task<List<Todo>> GetAllTasksAsync();
        Task<Todo?> GetTaskByIdAsync(Guid id);
        Task AddTaskAsync(Todo todo);
        Task UpdateTaskAsync(Todo todo);
        Task DeleteTaskAsync(Guid id);
    }
}