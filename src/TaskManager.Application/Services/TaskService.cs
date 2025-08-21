using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _repo;

        public TaskService(ITaskRepository repo) => _repo = repo;

        public async Task<List<Todo>> GetAllTasksAsync() => await _repo.GetAllTasksAsync();

        public async Task<Todo?> GetTaskByIdAsync(Guid id) => await _repo.GetTaskByIdAsync(id);

        public async Task AddTaskAsync(Todo todo) => await _repo.AddTaskAsync(todo);

        public async Task UpdateTaskAsync(Todo todo) => await _repo.UpdateTaskAsync(todo);

        public async Task DeleteTaskAsync(Guid id) => await _repo.DeleteTaskAsync(id);
    }
}