using API.IServices;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace API.Services
{
    public class RoleClaimService : IRoleClaimService
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleClaimService(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task AddClaimToRoleAsync(string roleName, Claim claim)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                await _roleManager.AddClaimAsync(role, claim);
            }
        }

        public async Task RemoveClaimFromRoleAsync(string roleName, Claim claim)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                await _roleManager.RemoveClaimAsync(role, claim);
            }
        }

        public async Task<IList<Claim>> GetClaimsByRoleAsync(string roleName)
        {
            var role = await _roleManager.FindByNameAsync(roleName);
            if (role != null)
            {
                return await _roleManager.GetClaimsAsync(role);
            }

            return new List<Claim>();
        }
    }
}
