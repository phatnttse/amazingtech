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
        public required decimal ContractSalary { get; set; } 
        public int LeaveDays { get; set; } = 0;
        public DateTime? CalculationDate { get; set; }
        public decimal TotalSalary { get; set; } 
    }
}
