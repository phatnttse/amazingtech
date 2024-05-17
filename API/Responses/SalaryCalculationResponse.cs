using API.Dtos;
using API.Models;
using System.ComponentModel.DataAnnotations;

namespace API.Responses
{
    public class SalaryCalculationResponse
    {
      
        public required string Id { get; set; }

        public UserResponse User { get; set; }

        public required decimal ContractSalary { get; set; }

        public int LeaveDays { get; set; }

        public int TotalWorkingDaysInMonth { get; set; }

        public required decimal TotalSalary { get; set; }


    }
}
