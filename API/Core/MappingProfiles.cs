using API.Dtos;
using API.Models;
using AutoMapper;


namespace API.Core
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<User, UserDto>();

            CreateMap<UserDto, User>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<RegisterDto, User>()
           .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
           .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));

            CreateMap<Attachment, AttachmentDto>().ReverseMap();

            CreateMap<Form, FormDto>().ReverseMap();

            CreateMap<Form, FormDto>()
            .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments));
        }
    }
}
