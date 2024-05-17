using API.Dtos;
using API.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<List<IdentityRole>>> GetRoles()
        {
            try
            {
                var roles = await _roleService.GetRoles();

                return Ok(roles);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<ActionResult<IdentityRole>> CreateRole(CreateRoleDto createRoleDto)
        {
            try
            {
                var newRole = await _roleService.CreateRole(createRoleDto);

                return Ok(newRole);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<IdentityRole>> GetRolesByUserId(string userId)
        {
            try
            {
                var role = await _roleService.GetRoleByUserId(userId);

                return Ok(role);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("user/{userId}")]
        public async Task<ActionResult> UpdateUserRole(string userId, UpdateUserRoleDto updateUserRoleDto)
        {
            try
            {
                await _roleService.UpdateUserRoleAsync(userId, updateUserRoleDto.RoleName);

                return Ok("User role updated successfully");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
