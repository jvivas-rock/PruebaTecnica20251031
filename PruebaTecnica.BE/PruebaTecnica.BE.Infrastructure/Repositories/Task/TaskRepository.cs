using Microsoft.EntityFrameworkCore;
using PruebaTecnica.BE.Application.Interfaces.Repositories;
using PruebaTecnica.BE.Domain.DTOs.Tasks;
using PruebaTecnica.BE.Infrastructure.Database;
using TaskEntity = PruebaTecnica.BE.Domain.Entities.Tasks.Task;

namespace PruebaTecnica.BE.Infrastructure.Repositories.Task
{
    public class TaskRepository : BaseRepository<TaskEntity>, ITaskRepository
    {
        public TaskRepository(DBContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskEntity>> GetByUserIdAsync(int userId)
        {
            return await _dbSet
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<IEnumerable<TaskEntity>> GetByStatusAsync(int userId, bool status)
        {
            return await _dbSet
                .Where(t => t.UserId == userId && t.Status == status)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();
        }

        public async Task<TaskEntity> GetByTaskCodeAsync(string taskCode)
        {
            return await _dbSet.FirstOrDefaultAsync(t => t.TaskCode == taskCode);
        }

        public async Task<TaskStatsDto> GetTaskStatsAsync(int userId)
        {
            var tasks = await _dbSet
                .Where(t => t.UserId == userId)
                .ToListAsync();

            var totalTasks = tasks.Count;
            var completedTasks = tasks.Count(t => t.Status);
            var pendingTasks = totalTasks - completedTasks;
            var completionPercentage = totalTasks > 0 ? (decimal)completedTasks / totalTasks * 100 : 0;

            var recentTasks = tasks
                .OrderByDescending(t => t.CreatedAt)
                .Take(5)
                .Select(t => new TaskDto
                {
                    Id = t.Id,
                    TaskCode = t.TaskCode,
                    Title = t.Title,
                    Description = t.Description,
                    Status = t.Status,
                    DueDate = t.DueDate,
                    Priority = t.Priority,
                    CreatedAt = t.CreatedAt,
                    UpdatedAt = t.UpdatedAt,
                    UserId = t.UserId
                })
                .ToList();

            return new TaskStatsDto
            {
                TotalTasks = totalTasks,
                CompletedTasks = completedTasks,
                PendingTasks = pendingTasks,
                CompletionPercentage = Math.Round(completionPercentage, 2),
                RecentTasks = recentTasks
            };
        }
    }
}