using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class UpdateSalaryDto
    {
        public required string Id { get; set; }

        [Range(0, int.MaxValue)]
        public required decimal ContractSalary { get; set; }

        [Range(0, 10)]
        public int LeaveDays { get; set; }

        [Range(0, 31)]
        public int TotalWorkingDaysInMonth { get; set; }
    }
}
