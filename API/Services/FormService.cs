using API.Dtos;
using API.Models;
using API.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class FormService : IFormService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public FormService(IUnitOfWork unitOfWork, IMapper mapper, IAttachmentService attachmentService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _attachmentService = attachmentService;
        }

        public async Task<Form> SentForm(FormDto formDto, ICollection<IFormFile> files)
        {
            var form = _mapper.Map<Form>(formDto);

            await _unitOfWork.GetRepository<Form>().CreateAsync(form);

            var attachments = await _attachmentService.AddAttachments(files, form.Id);
                
            await _unitOfWork.SaveChangesAsync();

            return form;
        }


        public async Task<FormDto?> ApproveForm(string formId)
        {
            var form = await _unitOfWork.GetRepository<Form>().GetByIdAsync(formId);

            if (form == null)
            {
                throw new ArgumentException("Form not found");
            }

            if (form.IsApproved)
            {
                throw new InvalidOperationException("Form has already been approved");
            }

            if (form.IsRejected)
            {
                // Remove rejection status and rejected date
                form.IsRejected = false;
                form.RejectedAt = null;
            }

            form.IsApproved = true;
            form.ApprovedAt = DateTime.Now;

            await _unitOfWork.GetRepository<Form>().UpdateAsync(form);

            await _unitOfWork.SaveChangesAsync();

            var formDto = await _unitOfWork.GetRepository<Form>().GetQueryable()
           .Where(f => f.Id == formId)
           .ProjectTo<FormDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync();

            return formDto;
        }


        public async Task<FormDto?> RejectForm(string formId)
        {
            var form = await _unitOfWork.GetRepository<Form>().GetByIdAsync(formId);

            if (form == null)
            {
                throw new ArgumentException("Form not found");
            }

            if (form.IsRejected)
            {
                throw new InvalidOperationException("Form has already been rejected");
            }

            if (form.IsApproved)
            {
                // Remove approval status and approval date
                form.IsApproved = false;
                form.ApprovedAt = null;
            }

            form.IsRejected = true;
            form.RejectedAt = DateTime.Now;

            await _unitOfWork.GetRepository<Form>().UpdateAsync(form);

            await _unitOfWork.SaveChangesAsync();

            var formDto = await _unitOfWork.GetRepository<Form>().GetQueryable()
                      .Where(f => f.Id == formId)
                      .ProjectTo<FormDto>(_mapper.ConfigurationProvider)
                      .SingleOrDefaultAsync();

            return formDto;
        }

        public async Task<List<FormDto>> GetAllForms()
        {
            var forms = await _unitOfWork.GetRepository<Form>().GetQueryable()
                 .ProjectTo<FormDto>(_mapper.ConfigurationProvider)
                 .ToListAsync();

            return forms;

        }

        public async Task<FormDto> GetFormById(string formId)
        {
            var existingForm = await _unitOfWork.GetRepository<Form>().GetQueryable()
           .Where(f => f.Id == formId)
           .ProjectTo<FormDto>(_mapper.ConfigurationProvider)
           .SingleOrDefaultAsync();

            if (existingForm == null) throw new ArgumentException("Form not found");

            return existingForm;
        }

        public Task<Form> DeleteForm(string formId, string attachmentId)
        {
            throw new NotImplementedException();
        }
    }

}

