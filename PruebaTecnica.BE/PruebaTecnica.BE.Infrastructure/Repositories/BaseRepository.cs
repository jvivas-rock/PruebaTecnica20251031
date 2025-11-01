using Microsoft.EntityFrameworkCore;
using PruebaTecnica.BE.Application.Interfaces.Repositories;
using PruebaTecnica.BE.Domain.Common;
using PruebaTecnica.BE.Infrastructure.Database;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaTecnica.BE.Infrastructure.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DBContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(DBContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async System.Threading.Tasks.Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async System.Threading.Tasks.Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async System.Threading.Tasks.Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public virtual async System.Threading.Tasks.Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public virtual async System.Threading.Tasks.Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}