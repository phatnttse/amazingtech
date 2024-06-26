﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Forms")]
    public class Form
    {
        public Form()
        {
            Id = Guid.NewGuid().ToString();  
        }

        [Key]
        public string Id { get; set; }

        [ForeignKey(nameof(User))]
        public required string UserId { get; set; }
        public User? User { get; set; }

        [Required]
        public required FormType Type { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [DefaultValue(false)]
        public bool IsApproved { get; set; } = false;

        [DefaultValue(false)]
        public bool IsRejected { get; set; } = false;

        public DateTime? ApprovedAt { get; set; }

        public DateTime? RejectedAt { get; set; }

        // Danh sách tệp đính kèm
        public ICollection<Attachment>? Attachments { get; set; }
    }
}
