using API.Dtos;
using API.IServices;
using API.Models;
using API.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;


namespace API.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public RoleService(RoleManager<IdentityRole> roleManager, IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IdentityRole> CreateRole(CreateRoleDto createRoleDto)
        {
            if (await _roleManager.RoleExistsAsync(createRoleDto.Name))
                throw new Exception("Role already exists");

            var newRole = new IdentityRole { Name = createRoleDto.Name };
            var result = await _roleManager.CreateAsync(newRole);

            if (result.Succeeded)
            {
                await _unitOfWork.SaveChangesAsync();
                return newRole;
            }
            else
            {
                throw new Exception("Failed to create role");
            }
        }

        public async Task<List<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IdentityRole?> GetRoleByUserId(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
                throw new Exception("User not found");

            var roles = await _userManager.GetRolesAsync(user);

            if (roles == null || !roles.Any())
                throw new Exception("User has no role assigned");

            var roleName = roles.First();

            var role = await _roleManager.FindByNameAsync(roleName);

            return role;
        }
    }
}
