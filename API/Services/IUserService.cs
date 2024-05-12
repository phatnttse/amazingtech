using API.Dtos;
using API.Models;

namespace API.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(string id);
        Task<User> CreateUserAsync(UserDto userDto);
        Task<User> UpdateUserAsync(string id, UserDto userDto);
        Task DeleteUserAsync(string id);

        Task<User> Register(RegisterDto registerDto);

        Task<string> Login(LoginDto loginDto);
    }
}
