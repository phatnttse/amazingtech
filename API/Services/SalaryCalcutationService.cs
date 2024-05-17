using API.Dtos;
using API.Models;
using API.Repositories;
using API.Responses;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;


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

        public async Task<SalaryCalculationResponse?> GetSalaryByUserId(string userId)
        {
            var existingUser = await _unitOfWork.GetRepository<User>().GetByIdAsync(userId);

            if (existingUser == null) throw new Exception("User not found");
            if (!existingUser.Active) throw new Exception("User is deleted");

            var salaryCalculation = await _unitOfWork.GetRepository<SalaryCalculation>().GetQueryable()
                .Include(sc => sc.User)
                .Where(sc => sc.UserId == userId)
                .OrderByDescending(sc => sc.CalculationDate)
                .FirstOrDefaultAsync();

            if (salaryCalculation == null) return null;

            return _mapper.Map<SalaryCalculationResponse>(salaryCalculation);
        }

        public async Task<List<SalaryCalculationResponse>> GetAllSalaryCalculations()
        {
            var salaryCalculations = await _unitOfWork.GetRepository<SalaryCalculation>()
                .GetQueryable()
                .Include(sc => sc.User)
                .Where(sc => sc.User.Active == true)
                .ProjectTo<SalaryCalculationResponse>(_mapper.ConfigurationProvider) // Thực hiện projection
                .ToListAsync();

            return salaryCalculations;
        }

        public async Task<List<SalaryCalculationResponse>> GetSalaryByPosition(Position position)
        {
            var salaryCalculations = await _unitOfWork.GetRepository<SalaryCalculation>()
                .GetQueryable()
                .Include(sc => sc.User)
                .Where(sc => sc.User.Position == position && sc.User.Active == true)
                .ProjectTo<SalaryCalculationResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return salaryCalculations;
        }

        public async Task<SalaryCalculationResponse> CreateSalaryCalculation(SalaryCalculationDto salaryCalculationDto)
        {
            var existingUser = await _unitOfWork.GetRepository<User>().GetByIdAsync(salaryCalculationDto.UserId);

            if (existingUser == null) throw new Exception("User not found");
            if (!existingUser.Active) throw new Exception("User is deleted");

            var existingSalaryCalculation = await _unitOfWork.GetRepository<SalaryCalculation>().GetQueryable()
                .Where(sc => sc.UserId == salaryCalculationDto.UserId)
                .FirstOrDefaultAsync();

            if (existingSalaryCalculation != null)
            {
                throw new InvalidOperationException("A salary calculation already exists for this user.");
            }

            var salaryCalculation = _mapper.Map<SalaryCalculation>(salaryCalculationDto);

            CalculateTotalSalary(salaryCalculation);

            await _unitOfWork.GetRepository<SalaryCalculation>().CreateAsync(salaryCalculation);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SalaryCalculationResponse>(salaryCalculation);
        }


        public async Task<SalaryCalculationResponse> UpdateSalaryCalculation(string userId, UpdateSalaryDto updateSalaryDto)
        {

            var existingUser = await _unitOfWork.GetRepository<User>().GetByIdAsync(userId);

            if (existingUser == null) throw new Exception("User not found");
            if (!existingUser.Active) throw new Exception("User is deleted");

            var existingSalary = await _unitOfWork.GetRepository<SalaryCalculation>().GetByIdAsync(updateSalaryDto.Id); 

            if (existingSalary == null) throw new Exception("Salary not found");

            existingSalary.ContractSalary = updateSalaryDto.ContractSalary;
            existingSalary.LeaveDays = updateSalaryDto.LeaveDays;
            existingSalary.TotalWorkingDaysInMonth = updateSalaryDto.TotalWorkingDaysInMonth;

            CalculateTotalSalary(existingSalary);

            await _unitOfWork.GetRepository<SalaryCalculation>().UpdateAsync(existingSalary);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<SalaryCalculationResponse>(existingSalary);
        }

      

        private void CalculateTotalSalary(SalaryCalculation salaryCalculation)
        {
            var dailySalary = salaryCalculation.ContractSalary / salaryCalculation.TotalWorkingDaysInMonth;

            var totalSalary = salaryCalculation.ContractSalary - (salaryCalculation.LeaveDays * dailySalary);

            salaryCalculation.TotalSalary = Math.Round(totalSalary, 0);
        }

       


    }
}
