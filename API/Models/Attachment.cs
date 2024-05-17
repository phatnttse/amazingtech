using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    [Table("Attachments")]
    public class Attachment
    {
        public Attachment()
        {
            
        }
        [Key]
        public required string Id { get; set; }

        [Required]
        public required string Url { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.Now;

        public required string FormId { get; set; } // Khóa ngoại đến đơn

        [ForeignKey("FormId")]
        public virtual Form? Form { get; set; } // Đơn mà tệp đính kèm thuộc về
    }
}
