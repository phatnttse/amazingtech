using API.Dtos;
using API.Models;

namespace API.Services
{
    public interface IFormService
    {
        Task<Form> SentForm(FormDto formDto, ICollection<IFormFile> files);
        Task<FormDto?> ApproveForm(string formId);
        Task<FormDto?> RejectForm(string formId);
        Task<List<FormDto>> GetAllForms();
        Task<FormDto> GetFormById(string formId);
        Task<Form> DeleteForm(string formId, string attachmentId);

    }
}
