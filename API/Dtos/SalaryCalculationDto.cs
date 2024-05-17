using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class SalaryCalculationDto
    {
        public SalaryCalculationDto()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public required string UserId { get; set; }

        [Range(0, int.MaxValue)]
        public required decimal ContractSalary { get; set; }

        [Range(0, 10)]
        public int LeaveDays { get; set; }

        [Range(0, 31)]
        public int TotalWorkingDaysInMonth { get; set; }

       
    }
}
