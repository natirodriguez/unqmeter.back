using AutoMapper;
using UnqMeterAPI.Interfaces;
using UnqMeterAPI.Models;

namespace UnqMeterAPI.Services
{
    public class RespuestaParticipanteService : IRespuestaParticipanteService
    {
        private readonly IMapper _mapper;
        private IRepositoryManager<Respuesta> _respuestaRepository;
        private IRepositoryManager<Slyde> _slydeRepository;

        public RespuestaParticipanteService(IMapper mapper, IRepositoryManager<Respuesta> respuestaRepository, IRepositoryManager<Slyde> slydeRepository)
        {
            _mapper = mapper;
            _respuestaRepository = respuestaRepository;
            _slydeRepository = slydeRepository;
        }

        public IList<Slyde> GetSlydesNoRespondidas(int idPresentacion, string ipUsuario)
        {
            var slydes = _slydeRepository.FindBy(x => x.Presentacion.Id == idPresentacion).ToList();
            var respuestas = _respuestaRepository.FindBy(x => x.Participante == ipUsuario).ToList();

            var slydesSinRespuestas = slydes.Where(f1 => respuestas.All(f2 => f2.Slyde.Id != f1.Id));

            return slydesSinRespuestas.ToList();
        }
    }
}
