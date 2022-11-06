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

            CreateMap<Presentacion, PresentacionDTO>()
            .ForMember(p => p.nombre, opt => opt.MapFrom(x => x.Nombre));

            CreateMap<Slyde, SlydeDTO>()
            .ForMember(p => p.Id, opt => opt.MapFrom(x => x.Id));

            CreateMap<OpcionesSlyde, OpcionesSlydeDTO>()
            .ForMember(p => p.Id, opt => opt.MapFrom(x => x.Id));

            CreateMap<Respuesta, RespuestaDTO>()
            .ForMember(p => p.id, opt => opt.MapFrom(x => x.Id));

            CreateMap<DescripcionRespuesta, DescripcionRespuestaDTO>()
            .ForMember(p => p.id, opt => opt.MapFrom(x => x.Id));
        }
    }
}
