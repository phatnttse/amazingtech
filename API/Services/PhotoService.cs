using API.Dtos;
using API.Models;
using API.Repositories;
using AutoMapper;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using API.Core;

namespace API.Services
{

    public class PhotoService : IPhotoService
    {

        private readonly Cloudinary _cloudinary;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PhotoService(IOptions<CloudinarySettings> config, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

            var account = new Account(
                config.Value.CloudName,
                config.Value.ApiKey,
                config.Value.ApiSecret
                );
            _cloudinary = new Cloudinary(account);


        }

        public async Task<PhotoUploadDto?> AddPhoto(IFormFile file)
        {
            if (file.Length > 0)
            {
                await using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill")
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception(uploadResult.Error.Message);
                }

                var attachmentRepository = _unitOfWork.GetRepository<Photo>();

                var photoUploadDto = new PhotoUploadDto
                {
                    PublicId = uploadResult.PublicId,
                    Url = uploadResult.SecureUrl.ToString(),
                };

                var photo = _mapper.Map<Photo>(photoUploadDto);

                await attachmentRepository.CreateAsync(photo);

                await _unitOfWork.SaveChangesAsync();

                return photoUploadDto;

            }

            return null;
        }

        public async Task<string?> DeletePhoto(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deleteParams);
            return result.Result == "ok" ? result.Result : null;
        }
    }
}




