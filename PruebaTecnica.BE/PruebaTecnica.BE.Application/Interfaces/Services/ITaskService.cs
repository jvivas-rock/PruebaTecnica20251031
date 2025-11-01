using PruebaTecnica.BE.Domain.DTOs.Tasks;

namespace PruebaTecnica.BE.Application.Interfaces.Services
{
    public interface ITaskService
    {
        Task<TaskDto> CreateTaskAsync(int userId, CreateTaskDto createTaskDto);
        Task<IEnumerable<TaskDto>> GetUserTasksAsync(int userId);
        Task<IEnumerable<TaskDto>> GetTasksByStatusAsync(int userId, bool status);
        Task<TaskDto> GetTaskByIdAsync(int id, int userId);
        Task<TaskDto> UpdateTaskAsync(int id, int userId, UpdateTaskDto updateTaskDto);
        Task DeleteTaskAsync(int id, int userId);
        Task<TaskStatsDto> GetTaskStatsAsync(int userId);
    }
}
