using API.Core;
using API.Dtos;
using API.Models;
using API.Repositories;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Options;

namespace API.Services
{
    public class AttachmentService : IAttachmentService
    {

        private readonly Cloudinary _cloudinary;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AttachmentService(IOptions<CloudinarySettings> config, IUnitOfWork unitOfWork, IMapper mapper)
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

        public async Task<List<AttachmentDto>?> AddAttachments(ICollection<IFormFile> files, string formId)
        {
            var attachmentDtos = new List<AttachmentDto>();

            foreach (var file in files)
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

                    //var attachmentDto = new AttachmentDto
                    //{
                    //    Id = uploadResult.PublicId,
                    //    Url = uploadResult.SecureUrl.ToString(),
                    //    UploadedAt = uploadResult.CreatedAt,
                    //    FormId = formId
                    //};

                    var attachment = new Attachment
                    {

                        Id = uploadResult.PublicId,
                        Url = uploadResult.SecureUrl.ToString(),
                        UploadedAt = uploadResult.CreatedAt,
                        FormId = formId
                    };

                    var attachmentDto = _mapper.Map<AttachmentDto>(attachment);

                    var attachmentRepository = _unitOfWork.GetRepository<Attachment>();

                    await attachmentRepository.CreateAsync(attachment);

                    attachmentDtos.Add(attachmentDto);

                }
            }

            if (attachmentDtos != null)
            {
                return attachmentDtos;
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
