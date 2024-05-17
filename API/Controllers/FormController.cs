using API.Dtos;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FormController : ControllerBase
    {
        private readonly IFormService _formService;
        private readonly IMapper _mapper;

        public FormController(IFormService formService, IMapper mapper)
        {
            _formService = formService;
            _mapper = mapper;
        }

        [HttpPost("sent-form")]
        public async Task<ActionResult<FormDto>> SentForm([FromForm] FormDto formDto, [FromForm] ICollection<IFormFile> files)
        {
            try
            {
                var form = await _formService.SentForm(formDto, files);

                var response = _mapper.Map<FormDto>(form);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("approve-form/{formId}")]
        public async Task<ActionResult<FormDto>> ApproveForm(string formId)
        {
            try
            {
                var form = await _formService.ApproveForm(formId);

                return Ok(form);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut("reject-form/{formId}")]
        public async Task<ActionResult<FormDto>> RejectForm(string formId)
        {
            try
            {
                var form = await _formService.RejectForm(formId);

                return Ok(form);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public async Task<ActionResult<List<FormDto>>> GetAllForms()
        {

            try
            {
                var forms = await _formService.GetAllForms();

                if (forms == null) return NoContent();

                return Ok(forms);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpGet("{formId}")]
        public async Task<ActionResult<FormDto>> GetFormById(string formId)
        {

            try
            {
                var form = await _formService.GetFormById(formId);

                var response = _mapper.Map<FormDto>(form);

                return Ok(response);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }



    }
}
