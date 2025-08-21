using TaskManager.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<Todo>> GetAllTasksAsync();
        Task AddTaskAsync(Todo todo);
    }
}