using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    [Table("SalaryCalculation")]
    public class SalaryCalculation
    {
        public SalaryCalculation()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public required string Id { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public required string UserId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, int.MaxValue)]
        public decimal ContractSalary { get; set; }

        [Range(0, 10)]
        public int LeaveDays { get; set; }

        [Range(0, 31)]
        public int TotalWorkingDaysInMonth { get; set; }

        public DateTime CalculationDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Range(0, int.MaxValue)]
        public decimal TotalSalary { get; set; }

        public virtual User User { get; set; }
    }
}
