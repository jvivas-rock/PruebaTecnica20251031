//using System.Threading.Tasks;
using PruebaTecnica.BE.Domain.DTOs.Tasks;
using TaskItem = PruebaTecnica.BE.Domain.Entities.Tasks.Task;

namespace PruebaTecnica.BE.Application.Interfaces.Repositories
{
    public interface ITaskRepository : IRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetByUserIdAsync(int userId);
        Task<IEnumerable<TaskItem>> GetByStatusAsync(int userId, bool status);
        Task<TaskStatsDto> GetTaskStatsAsync(int userId);
        Task<TaskItem> GetByTaskCodeAsync(string taskCode);
    }
}
