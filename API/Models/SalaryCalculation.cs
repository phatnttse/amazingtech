using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("SalaryCalculation")]
    public class SalaryCalculation
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public Guid UserId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal ContractSalary { get; set; }

        public int LeaveDays { get; set; }

        public DateTime CalculationDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalSalary { get; set; }

        public virtual User User { get; set; }
    }
}
