using AutoMapper;
using UnqMeterAPI.Interfaces;
using UnqMeterAPI.Models;

namespace UnqMeterAPI.Services
{
    public class RespuestaParticipanteService : IRespuestaParticipanteService
    {
        private readonly IMapper _mapper;
        private IRepositoryManager<Respuesta> _respuestaRepository;

        public RespuestaParticipanteService(IMapper mapper, IRepositoryManager<Respuesta> respuestaRepository)
        {
            _mapper = mapper;
            _respuestaRepository = respuestaRepository;
        }

    }
}
