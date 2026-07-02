using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagement.Api.Application.Models;
using TaskManagement.Api.Domain.Entities;
using TaskManagement.Api.Infrastructure.Persistence;

namespace TaskManagement.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TasksController(TaskDbContext dbContext, ILogger<TasksController> logger) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<TaskItem>>> GetAll()
    {
        return Ok(await dbContext.Tasks.OrderBy(x => x.Id).ToListAsync());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TaskItem>> GetById(int id)
    {
        var task = await dbContext.Tasks.FindAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<TaskItem>> Create([FromBody] TaskItemRequest request)
    {
        var task = new TaskItem
        {
            Title = request.Title,
            Description = request.Description,
            DueDate = request.DueDate,
            IsCompleted = request.IsCompleted
        };

        dbContext.Tasks.Add(task);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Task {TaskId} created by {User}", task.Id, User.Identity?.Name);

        return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TaskItemRequest request)
    {
        var task = await dbContext.Tasks.FindAsync(id);
        if (task is null)
        {
            return NotFound();
        }

        task.Title = request.Title;
        task.Description = request.Description;
        task.DueDate = request.DueDate;
        task.IsCompleted = request.IsCompleted;

        await dbContext.SaveChangesAsync();

        logger.LogInformation("Task {TaskId} updated by {User}", task.Id, User.Identity?.Name);

        return NoContent();
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        var task = await dbContext.Tasks.FindAsync(id);
        if (task is null)
        {
            return NotFound();
        }

        dbContext.Tasks.Remove(task);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Task {TaskId} deleted by {User}", task.Id, User.Identity?.Name);

        return NoContent();
    }
}
