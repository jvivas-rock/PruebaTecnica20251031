using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnica.BE.Application.Interfaces.Repositories;
using PruebaTecnica.BE.Application.Interfaces.Services;
using PruebaTecnica.BE.Application.Services;
using PruebaTecnica.BE.Infrastructure.Database;
using PruebaTecnica.BE.Infrastructure.Repositories.Task;
using PruebaTecnica.BE.Infrastructure.Repositories.UserRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BE.Infrastructure.IOC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Database
            services.AddDbContext<DBContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Repositories
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITaskRepository, TaskRepository>();

            // Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITaskService, TaskService>();

            return services;
        }
    }
}
