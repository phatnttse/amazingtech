using API.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class FormDto
    {
        public FormDto()
        {
            Id = Guid.NewGuid().ToString();
        }
        public required string Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        public FormType Type { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public bool IsApproved { get; set; }

        public bool IsRejected { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public DateTime? RejectedAt { get; set; }

        // Danh sách tệp đính kèm
        public ICollection<AttachmentDto>? Attachments { get; set; }
    }
}
