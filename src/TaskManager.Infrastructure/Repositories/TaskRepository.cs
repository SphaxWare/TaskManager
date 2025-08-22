using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;

        public TaskRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<List<Todo>> GetAllTasksAsync() =>
            await _context.Todos.ToListAsync();

        public async Task<Todo?> GetTaskByIdAsync(Guid id) =>
    await _context.Todos.FindAsync(id);

        public async Task AddTaskAsync(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTaskAsync(Todo todo)
        {
            var existing = await _context.Todos.FindAsync(todo.Id);
            if (existing == null)
                throw new Exception("Task not found");

            existing.Title = todo.Title;
            existing.IsCompleted = todo.IsCompleted;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteTaskAsync(Guid id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo != null)
            {
                _context.Todos.Remove(todo);
                await _context.SaveChangesAsync();
            }
        }
    }
}