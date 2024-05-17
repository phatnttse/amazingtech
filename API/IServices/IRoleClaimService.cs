using System.Security.Claims;

namespace API.IServices
{
    public interface IRoleClaimService
    {
        Task AddClaimToRoleAsync(string roleName, Claim claim);
        Task RemoveClaimFromRoleAsync(string roleName, Claim claim);
        Task<IList<Claim>> GetClaimsByRoleAsync(string roleName);
    }
}
