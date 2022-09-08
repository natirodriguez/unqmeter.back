using AutoMapper;
using UnqMeterAPI.DTO;
using UnqMeterAPI.Models;

namespace UnqMeterAPI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistration, User>()
            .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));
        }
    }
}
