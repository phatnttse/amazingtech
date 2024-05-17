using API.Dtos;
using API.Models;
using API.Responses;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class SalaryCalculationController : ControllerBase
    {
        private readonly ISalaryCalculationService _salaryCalculationService;

        public SalaryCalculationController(ISalaryCalculationService salaryCalculationService)
        {
            _salaryCalculationService = salaryCalculationService;
        }

        [Authorize(Roles = "Manager, Employee")]    
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<SalaryCalculationDto>> GetSalaryByUserId(string userId)
        {
            try
            {
                var salaryCalculation = await _salaryCalculationService.GetSalaryByUserId(userId);

                if (salaryCalculation == null)
                {
                    return NotFound();
                }
                return Ok(salaryCalculation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpGet("position/{position}")]
        public async Task<ActionResult<List<SalaryCalculationDto>>> GetSalaryByPosition(Position position)
        {
            try
            {
                var salaryCalculations = await _salaryCalculationService.GetSalaryByPosition(position);

                return Ok(salaryCalculations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpGet]
        public async Task<ActionResult<List<SalaryCalculationResponse>>> GetAllSalaryCalculations()
        {
            try
            {
                var salaryCalculations = await _salaryCalculationService.GetAllSalaryCalculations();

                return Ok(salaryCalculations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPost("calculate-salary")]
        public async Task<ActionResult<SalaryCalculationResponse>> CreateSalaryCalculation(SalaryCalculationDto salaryCalculationDto)
        {
            try
            {
                var createdSalaryCalculation = await _salaryCalculationService.CreateSalaryCalculation(salaryCalculationDto);

                return Ok(createdSalaryCalculation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Manager")]
        [HttpPut("user/{userId}")]
        public async Task<ActionResult<SalaryCalculationResponse>> UpdateSalaryCalculation(string userId, UpdateSalaryDto updateSalaryDto)
        {
           
            try
            {
                var updatedSalaryCalculation = await _salaryCalculationService.UpdateSalaryCalculation(userId , updateSalaryDto);

                return Ok(updatedSalaryCalculation);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
