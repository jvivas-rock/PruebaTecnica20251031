using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnica.BE.Application.Interfaces.Services;

namespace TaskAppAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : BaseApiController
    {
        private readonly ITaskService _taskService;

        public DashboardController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("statistics")]
        public async Task<IActionResult> GetStatistics()
        {
            try
            {
                var userId = GetUserId();
                var stats = await _taskService.GetTaskStatsAsync(userId);
                return Ok(stats);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching dashboard statistics", error = ex.Message });
            }
        }

        [HttpGet("recent-tasks")]
        public async Task<IActionResult> GetRecentTasks()
        {
            try
            {
                var userId = GetUserId();
                var tasks = await _taskService.GetUserTasksAsync(userId);
                var recentTasks = tasks.OrderByDescending(t => t.CreatedAt).Take(5);
                return Ok(recentTasks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while fetching recent tasks", error = ex.Message });
            }
        }
    }
}
