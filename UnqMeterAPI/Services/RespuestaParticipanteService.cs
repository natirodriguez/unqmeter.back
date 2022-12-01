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
        private IRepositoryManager<OpcionesSlyde> _opcionesSlydeRepository; 

        public RespuestaParticipanteService(IRepositoryManager<Respuesta> respuestaRepository, IRepositoryManager<Slyde> slydeRepository,
            IRepositoryManager<DescripcionRespuesta> descripcionRepository, IRepositoryManager<OpcionesSlyde> opcionesSlydeRepository)
        {
            _respuestaRepository = respuestaRepository;
            _slydeRepository = slydeRepository;
            _descripcionRepository = descripcionRepository;
            _opcionesSlydeRepository = opcionesSlydeRepository; 
        }

        public IList<Slyde> GetSlydesSinRespuestas(int idPresentacion, string ipUsuario)
        {
            var slydes = _slydeRepository.FindBy(x => x.Presentacion.Id == idPresentacion).ToList();
            var slydesHabilitadas = slydes.Where(x => x.HabilitadoParaResponder).ToList();

            var slydesSinRespuestas = new List<Slyde>();
            foreach (var slyde in slydesHabilitadas)
            {
                var respuestasParticipante = _respuestaRepository.FindBy(y => y.Participante == ipUsuario).ToList();
                var respuesta = respuestasParticipante.Where(x => x.Slyde != null && x.Slyde.Id == slyde.Id).FirstOrDefault(); 

                if(respuesta == null)
                {
                    slyde.OpcionesSlydes = _opcionesSlydeRepository.FindBy(x => x.Slyde.Id == slyde.Id).ToList();
                    slydesSinRespuestas.Add(slyde);
                }
            }

            return slydesSinRespuestas;
        }

        public Respuesta SaveRespuesta(RespuestaDTO respuestaDTO)
        {
            var slyde = _slydeRepository.FindBy(x => x.Id == respuestaDTO.slydeId).First();

            var respuesta = new Respuesta();
            respuesta.Participante = respuestaDTO.participante;
            respuesta.Slyde = slyde;
            respuesta.FechaCreacion = DateTime.Now;
            respuesta.DescripcionGeneral = respuestaDTO.descripcionGeneral;

            if(slyde.TipoPregunta == Enums.TipoPregunta.MULTIPLE_CHOICE)
            {
                var opcionSlyde = _opcionesSlydeRepository.FindBy(x => x.Id == respuestaDTO.opcionElegidaId).FirstOrDefault();
                respuesta.OpcionElegida = opcionSlyde;
            }

            _respuestaRepository.Add(respuesta);
            _respuestaRepository.Save();

            switch (slyde.TipoPregunta)
            {
                case Enums.TipoPregunta.WORD_CLOUD:
                    SaveDescripcionRespuesta(respuestaDTO.descripcionesRespuesta, respuesta);
                    break;
                case Enums.TipoPregunta.RANKING:
                    SaveDescripcionRespuesta(respuestaDTO.descripcionesRespuesta, respuesta);
                    break;
                default:
                    break;
            }

            return respuesta;
        }

        public List<DescripcionRespuesta>? GetDescriptionAnswer (int answerId)
        {
            List<DescripcionRespuesta>? answersDescription = new List<DescripcionRespuesta>();
            answersDescription = _descripcionRepository?.FindBy(x => x.Respuesta.Id == answerId).ToList();
            
            return answersDescription;
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
