using API.Dtos;
using API.Models;
using API.Responses;

namespace API.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(string id);
        Task<User> UpdateUserAsync(string id, UpdateUserDto updateUserDto);
        Task DeleteUserAsync(string id);
        Task<User?> Register(RegisterDto registerDto);
        Task<LoginResponse> Login(LoginDto loginDto);
    }
}
