using Microsoft.EntityFrameworkCore;
using PruebaTecnica.BE.Application.Interfaces.Repositories;
using PruebaTecnica.BE.Domain.Entities.Users;
using PruebaTecnica.BE.Infrastructure.Database;

namespace PruebaTecnica.BE.Infrastructure.Repositories.UserRepository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(DBContext context) : base(context)
        {
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _dbSet.AnyAsync(u => u.Username == username || u.Email == email);
        }
    }
}
