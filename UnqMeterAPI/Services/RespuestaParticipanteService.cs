using AutoMapper;
using UnqMeterAPI.DTO;
using UnqMeterAPI.Interfaces;
using UnqMeterAPI.Models;

namespace UnqMeterAPI.Services
{
    public class RespuestaParticipanteService : IRespuestaParticipanteService
    {
        private IRepositoryManager<Respuesta> _respuestaRepository;
        private IRepositoryManager<DescripcionRespuesta> _descripcionRepository;
        private IRepositoryManager<Slyde> _slydeRepository;

        public RespuestaParticipanteService(IRepositoryManager<Respuesta> respuestaRepository, IRepositoryManager<Slyde> slydeRepository,
            IRepositoryManager<DescripcionRespuesta> descripcionRepository)
        {
            _respuestaRepository = respuestaRepository;
            _slydeRepository = slydeRepository;
            _descripcionRepository = descripcionRepository;
        }

        public IList<Slyde> GetSlydesSinRespuestas(int idPresentacion, string ipUsuario)
        {
            var slydes = _slydeRepository.FindBy(x => x.Presentacion.Id == idPresentacion).ToList();
            var respuestas = _respuestaRepository.FindBy(y => y.Participante == ipUsuario).ToList();

            var slydesSinRespuestas = slydes.Where(f1 => respuestas.All(f2 => f2.Slyde.Id != f1.Id));

            return slydesSinRespuestas.ToList();
        }

        public Respuesta SaveRespuesta(RespuestaDTO respuestaDTO)
        {
            var slyde = _slydeRepository.FindBy(x => x.Id == respuestaDTO.slydeId).First();

            var respuesta = new Respuesta();
            respuesta.Participante = respuestaDTO.participante;
            respuesta.Slyde = slyde;
            respuesta.FechaCreacion = DateTime.Now;
            respuesta.DescripcionGeneral = respuestaDTO.descripcionGeneral; 

            _respuestaRepository.Add(respuesta);
            _respuestaRepository.Save();

            switch (slyde.TipoPregunta)
            {
                case Enums.TipoPregunta.WORK_CLOUD:
                    SaveDescripcionRespuesta(respuestaDTO.descripcionesRespuesta, respuesta);
                    break;
                default:
                    break;
            }

            return respuesta;
        }

        private void SaveDescripcionRespuesta(List<DescripcionRespuestaDTO> descripcionesDTO, Respuesta respuesta)
        {
            foreach(DescripcionRespuestaDTO dto in descripcionesDTO)
            {
                DescripcionRespuesta descripcionRespuesta = new DescripcionRespuesta();
                descripcionRespuesta.Respuesta = respuesta;
                descripcionRespuesta.Descripcion = dto.descripcion;

                _descripcionRepository.Add(descripcionRespuesta);
                _descripcionRepository.Save();
            }
        }
    }
}
