using API.Dtos;
using Microsoft.AspNetCore.Identity;

namespace API.IServices
{
    public interface IRoleService
    {
        Task<IdentityRole?> GetRoleByUserId(string userId);

        Task<List<IdentityRole>> GetRoles();

        Task<IdentityRole> CreateRole(CreateRoleDto createRoleDto);


    }
}
