﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class UserRole
    {
        public UserRole()
        {
            
        }

        [Key]
        public required Guid UserRoleId { get; set; }

        [ForeignKey(nameof(User))] 
        public required string UserId { get; set; }
        public User? User { get; set; }

        [ForeignKey(nameof(Role))]
        public Guid RoleId { get; set; }
        public Role? Role { get; set; }
    }
}
