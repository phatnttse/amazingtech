using API.Dtos;
using API.Models;
using API.Responses;


namespace API.Services
{
    public interface ISalaryCalculationService
    {
        Task<List<SalaryCalculationResponse>> GetAllSalaryCalculations();
        Task<SalaryCalculationResponse?> GetSalaryByUserId(string userId);
        Task<SalaryCalculationResponse> CreateSalaryCalculation(SalaryCalculationDto salaryCalculationDto);
        Task<SalaryCalculationResponse> UpdateSalaryCalculation(string userId, UpdateSalaryDto updateSalaryDto);
        Task<List<SalaryCalculationResponse>> GetSalaryByPosition(Position position);
    }
}
