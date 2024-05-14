using API.Dtos;


namespace API.Services
{
    public interface ISalaryCalculationService
    {
        Task<SalaryCalculationDto> GetSalaryCalculationById(string id);
        Task<List<SalaryCalculationDto>> GetAllSalaryCalculations();
        Task<SalaryCalculationDto> CreateSalaryCalculation(SalaryCalculationDto salaryCalculationDto);
        Task<SalaryCalculationDto> UpdateSalaryCalculation(SalaryCalculationDto salaryCalculationDto);
        Task DeleteSalaryCalculation(string id);
    }
}
