using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.DTOs;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskRepository _repo;
        public TasksController(ITaskRepository repo) => _repo = repo;

        [HttpGet]
        public async Task<IActionResult> GetTasks() => Ok(await _repo.GetAllTasksAsync());

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            var todo = new Todo { Title = command.Title };
            await _repo.AddTaskAsync(todo);
            return CreatedAtAction(nameof(GetTasks), new { id = todo.Id }, todo);
        }
    }
}