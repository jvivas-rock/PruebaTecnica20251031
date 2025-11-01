using PruebaTecnica.BE.Domain.DTOs.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTecnica.BE.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task<AuthResponseDto> RegisterAsync(CreateUserDto createUserDto);
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<UserDto> GetUserByIdAsync(int id);
    }
}
