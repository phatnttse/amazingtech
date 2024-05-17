using API.Dtos;

namespace API.Services
{
    public interface IAttachmentService
    {
        Task<List<AttachmentDto>?> AddAttachments(ICollection<IFormFile> files, string FormId);

        Task<string?> DeletePhoto(string publicId);
    }
}
