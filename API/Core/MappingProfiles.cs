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
        }
    }
}
