using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.BE.Application.Interfaces.Services;
using PruebaTecnica.BE.Application.Services;
using PruebaTecnica.BE.Domain.DTOs;
using PruebaTecnica.BE.Domain.DTOs.Tasks;
using TaskAppAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class TasksController : BaseApiController
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetUserTasks()
    {
        try
        {
            var userId = GetUserId();
            var tasks = await _taskService.GetUserTasksAsync(userId);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while fetching tasks", error = ex.Message });
        }
    }

    [HttpGet("status/{status}")]
    public async Task<IActionResult> GetTasksByStatus(bool status)
    {
        try
        {
            var userId = GetUserId();
            var tasks = await _taskService.GetTasksByStatusAsync(userId, status);
            return Ok(tasks);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while fetching tasks", error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        try
        {
            var userId = GetUserId();
            var task = await _taskService.GetTaskByIdAsync(id, userId);
            return Ok(task);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while fetching task", error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto createTaskDto)
    {
        try
        {
            var userId = GetUserId();
            var task = await _taskService.CreateTaskAsync(userId, createTaskDto);
            return CreatedAtAction(nameof(GetTaskById), new { id = task.Id }, task);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while creating task", error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskDto updateTaskDto)
    {
        try
        {
            var userId = GetUserId();
            var task = await _taskService.UpdateTaskAsync(id, userId, updateTaskDto);
            return Ok(task);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while updating task", error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        try
        {
            var userId = GetUserId();
            await _taskService.DeleteTaskAsync(id, userId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while deleting task", error = ex.Message });
        }
    }

    [HttpGet("stats")]
    public async Task<IActionResult> GetTaskStats()
    {
        try
        {
            var userId = GetUserId();
            var stats = await _taskService.GetTaskStatsAsync(userId);
            return Ok(stats);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while fetching stats", error = ex.Message });
        }
    }
}