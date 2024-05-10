using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("Forms")]
    public class Form
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; } 
        public User User { get; set; } 

        [Required]  
        public FormType Type { get; set; } 

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        [Required]
        public string? Title { get; set; }

        public string? Description { get; set; }

        public bool IsApproved { get; set; }

        public bool IsRejected { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public DateTime? RejectedAt { get; set; }

        // Danh sách tệp đính kèm
        public ICollection<Attachment> Attachments { get; set; }
    }
}
