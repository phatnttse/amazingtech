namespace API.Dtos
{
    public class RoleClaimDto
    {
        public required string RoleName { get; set; }
        public required string ClaimType { get; set; }
        public required string ClaimValue { get; set; }
    }
}
