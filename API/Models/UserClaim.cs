using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class UserClaim
    {
        public UserClaim()
        {
            
        }

        [Key]
        public required Guid UserClaimId { get; set; }

        [ForeignKey(nameof(User))]
        public required string UserId { get; set; }
        public User? User { get; set; }

        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
    }
}
