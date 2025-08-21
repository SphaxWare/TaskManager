using Microsoft.EntityFrameworkCore;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using TaskManager.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TaskManager.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TaskDbContext _context;
        public TaskRepository(TaskDbContext context) => _context = context;

        public async Task AddTaskAsync(Todo todo)
        {
            _context.Todos.Add(todo);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Todo>> GetAllTasksAsync()
        {
            return await _context.Todos.ToListAsync();
        }
    }
}