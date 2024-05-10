using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    [Table("Attachments")]
    public class Attachment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        public string FilePath { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.Now;

        public int FormId { get; set; } // Khóa ngoại đến đơn

        [ForeignKey("FormId")]
        public virtual Form Form { get; set; } // Đơn mà tệp đính kèm thuộc về
    }
}
