using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("RoleClaims")]
    public class RoleClaim
    {
        [Key]
        public Guid RoleClaimId { get; set; }

        [ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }
        public Role Role { get; set; }

        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
    }
}
