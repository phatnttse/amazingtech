using Microsoft.AspNetCore.Identity;


namespace API.Models
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        public string[] Claims { get; private set; }
    }
}
