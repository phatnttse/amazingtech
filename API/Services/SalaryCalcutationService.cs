using API.Dtos;
using API.Models;
using API.Repositories;
using AutoMapper;


namespace API.Services
{
    public class SalaryCalculationService : ISalaryCalculationService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public SalaryCalculationService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<SalaryCalculationDto> GetSalaryCalculationById(string id)
        {
            var salaryCalculation = await _unitOfWork.GetRepository<SalaryCalculation>().GetByIdAsync(id);

            return _mapper.Map<SalaryCalculationDto>(salaryCalculation);
        }

        public async Task<List<SalaryCalculationDto>> GetAllSalaryCalculations()
        {
            var salaryCalculations = await _unitOfWork.GetRepository<SalaryCalculation>().GetAllAsync();

            return _mapper.Map<List<SalaryCalculationDto>>(salaryCalculations);
        }

        public async Task<SalaryCalculationDto> CreateSalaryCalculation(SalaryCalculationDto salaryCalculationDto)
        {
            var salaryCalculation = _mapper.Map<SalaryCalculation>(salaryCalculationDto);

            CalculateTotalSalary(salaryCalculation);

            await _unitOfWork.GetRepository<SalaryCalculation>().CreateAsync(salaryCalculation);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SalaryCalculationDto>(salaryCalculation);
        }

        public async Task<SalaryCalculationDto> UpdateSalaryCalculation(SalaryCalculationDto salaryCalculationDto)
        {
            var salaryCalculation = _mapper.Map<SalaryCalculation>(salaryCalculationDto);

            CalculateTotalSalary(salaryCalculation);

            await _unitOfWork.GetRepository<SalaryCalculation>().UpdateAsync(salaryCalculation);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SalaryCalculationDto>(salaryCalculation);
        }

        public async Task DeleteSalaryCalculation(string id)
        {
            await _unitOfWork.GetRepository<SalaryCalculation>().DeleteAsync(id);

            await _unitOfWork.SaveChangesAsync();
        }

        private void CalculateTotalSalary(SalaryCalculation salaryCalculation)
        {
            decimal dailySalary = salaryCalculation.ContractSalary / 30; // Giả sử mỗi tháng có 30 ngày

            salaryCalculation.TotalSalary = salaryCalculation.ContractSalary - (salaryCalculation.LeaveDays * dailySalary);
        }
    }
}
