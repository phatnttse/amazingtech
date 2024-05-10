using API.Dtos;
using API.Models;

namespace API.Services
{
    public interface IUserService
    {
        Task<List<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(Guid id);
        Task<User> CreateUserAsync(UserDto userDto);
        Task<User> UpdateUserAsync(Guid id, UserDto userDto);
        Task DeleteUserAsync(Guid id);
    }
}
