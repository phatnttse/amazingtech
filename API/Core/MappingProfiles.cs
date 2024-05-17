using API.Dtos;
using API.Models;
using API.Responses;
using AutoMapper;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<UpdateUserDto, User>().ReverseMap();
        CreateMap<User, UserResponse>().ReverseMap();
        CreateMap<RegisterDto, User>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));


        CreateMap<Attachment, AttachmentDto>().ReverseMap();


        CreateMap<Form, FormDto>().ReverseMap();
        CreateMap<Form, FormDto>()
            .ForMember(dest => dest.Attachments, opt => opt.MapFrom(src => src.Attachments));



        CreateMap<SalaryCalculation, SalaryCalculationDto>().ReverseMap();
        CreateMap<SalaryCalculation, UpdateSalaryDto>().ReverseMap();
        CreateMap<SalaryCalculationResponse, UpdateSalaryDto>().ReverseMap();
        CreateMap<SalaryCalculation, SalaryCalculationResponse>()
            .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User));
        CreateMap<SalaryCalculation, SalaryCalculationResponse>().ReverseMap();
       
    }
}
