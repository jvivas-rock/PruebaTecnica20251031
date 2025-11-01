using PruebaTecnica.BE.Domain.Entities.Users;
using TaskEntity = PruebaTecnica.BE.Domain.Entities.Tasks.Task;

namespace PruebaTecnica.BE.Application.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task<bool> UserExistsAsync(string username, string email);
    }
}
