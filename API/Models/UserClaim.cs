﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("UserClaims")]
    public class UserClaim
    {
        [Key]
        public Guid UserClaimId { get; set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

        public string? ClaimType { get; set; }
        public string? ClaimValue { get; set; }
    }
}