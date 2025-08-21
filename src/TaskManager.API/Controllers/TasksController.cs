using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly ITaskService _service;

        public TasksController(ITaskService service)
        {
            _service = service;
        }

        // GET: api/tasks
        [HttpGet]
        public async Task<IActionResult> GetTasks()
        {
            var tasks = await _service.GetAllTasksAsync();
            return Ok(tasks);
        }

        // GET: api/tasks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var task = await _service.GetTaskByIdAsync(id);
            if (task == null)
                return NotFound();
            return Ok(task);
        }

        // POST: api/tasks
        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] Todo todo)
        {
            if (todo == null)
                return BadRequest();

            await _service.AddTaskAsync(todo);
            return CreatedAtAction(nameof(GetTask), new { id = todo.Id }, todo);
        }

        // PUT: api/tasks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] Todo todo)
        {
            if (todo == null || todo.Id != id)
                return BadRequest();

            var existingTask = await _service.GetTaskByIdAsync(id);
            if (existingTask == null)
                return NotFound();

            await _service.UpdateTaskAsync(todo);
            return NoContent();
        }

        // DELETE: api/tasks/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var existingTask = await _service.GetTaskByIdAsync(id);
            if (existingTask == null)
                return NotFound();

            await _service.DeleteTaskAsync(id);
            return NoContent();
        }
    }
}