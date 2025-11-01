using PruebaTecnica.BE.Application.Interfaces.Repositories;
using PruebaTecnica.BE.Application.Interfaces.Services;
using PruebaTecnica.BE.Domain.DTOs;
using PruebaTecnica.BE.Domain.DTOs.Tasks;
using TaskItem = PruebaTecnica.BE.Domain.Entities.Tasks.Task;

namespace PruebaTecnica.BE.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }
        public async Task<TaskDto> CreateTaskAsync(int userId, CreateTaskDto createTaskDto)
        {
            var taskCode = GenerateTaskCode();

            var task = new TaskItem
            {
                TaskCode = taskCode,
                Title = createTaskDto.Title,
                Description = createTaskDto.Description,
                Status = false,
                DueDate = createTaskDto.DueDate,
                Priority = createTaskDto.Priority,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                CreatedBy = userId.ToString(),
                UpdatedBy = userId.ToString()
            };

            var createdTask = await _taskRepository.AddAsync(task);

            return MapToDto(createdTask);
        }
        public async Task<IEnumerable<TaskDto>> GetUserTasksAsync(int userId)
        {
            var tasks = await _taskRepository.GetByUserIdAsync(userId);
            return tasks.Select(MapToDto);
        }
        public async Task<IEnumerable<TaskDto>> GetTasksByStatusAsync(int userId, bool status)
        {
            var tasks = await _taskRepository.GetByStatusAsync(userId, status);
            return tasks.Select(MapToDto);
        }
        public async Task<TaskDto> GetTaskByIdAsync(int id, int userId)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null || task.UserId != userId)
                throw new KeyNotFoundException("Task not found");

            return MapToDto(task);
        }
        public async Task<TaskDto> UpdateTaskAsync(int id, int userId, UpdateTaskDto updateTaskDto)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null || task.UserId != userId)
                throw new KeyNotFoundException("Task not found");

            task.Title = updateTaskDto.Title;
            task.Description = updateTaskDto.Description;
            task.Status = updateTaskDto.Status;
            task.DueDate = updateTaskDto.DueDate;
            task.Priority = updateTaskDto.Priority;
            task.UpdatedAt = DateTime.UtcNow;
            task.UpdatedBy = userId.ToString();

            await _taskRepository.UpdateAsync(task);

            return MapToDto(task);
        }
        public async Task DeleteTaskAsync(int id, int userId)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null || task.UserId != userId)
                throw new KeyNotFoundException("Task not found");

            await _taskRepository.DeleteAsync(task);
        }
        public async Task<TaskStatsDto> GetTaskStatsAsync(int userId)
        {
            return await _taskRepository.GetTaskStatsAsync(userId);
        }
        private string GenerateTaskCode()
        {
            return $"TASK-{DateTime.UtcNow:yyyyMMddHHmmss}-{Guid.NewGuid().ToString()[..8].ToUpper()}";
        }
        private TaskDto MapToDto(TaskItem task)
        {
            return new TaskDto
            {
                Id = task.Id,
                TaskCode = task.TaskCode,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                DueDate = task.DueDate,
                Priority = task.Priority,
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt,
                UserId = task.UserId
            };
        }
    }
}
