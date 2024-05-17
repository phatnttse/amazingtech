using API.IServices;
using API.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _configuration;
        private readonly IRoleService _roleService;
        private readonly IRoleClaimService _roleClaimService;
        public TokenService(IConfiguration configuration, IRoleService roleService, IRoleClaimService roleClaimService)
        {
            _configuration = configuration;
            _roleService = roleService;
            _roleClaimService = roleClaimService;
        }


        public async Task<string> CreateToken(User user)
        {
            var userRole = _roleService.GetRoleByUserId(user.Id);
            
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Role, userRole.Result!.Name!)
            };

            var roleClaims = await _roleClaimService.GetClaimsByRoleAsync(userRole.Result!.Name!);
            claims.AddRange(roleClaims);


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
