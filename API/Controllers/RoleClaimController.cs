using API.Dtos;
using API.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RoleClaimController : ControllerBase
    {
        private readonly IRoleClaimService _roleClaimService;

        public RoleClaimController(IRoleClaimService roleClaimService)
        {
            _roleClaimService = roleClaimService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<IActionResult> AddClaimToRole([FromBody] RoleClaimDto roleClaimDto)
        {
            try
            {
                var claim = new Claim(roleClaimDto.ClaimType, roleClaimDto.ClaimValue);

                await _roleClaimService.AddClaimToRoleAsync(roleClaimDto.RoleName, claim);

                return Ok(new { message = "Claim added to role successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("claims/{roleName}")]
        public async Task<IActionResult> GetClaimsByRole([FromRoute] string roleName)
        {
            try
            {
                var claims = await _roleClaimService.GetClaimsByRoleAsync(roleName);

                return Ok(claims);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
