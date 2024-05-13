
using API.Dtos;

namespace API.Services
{
    public interface IPhotoService
    {
        Task<PhotoUploadDto?> AddPhoto(IFormFile file);

        Task<string?> DeletePhoto(string publicId);
    }
}
