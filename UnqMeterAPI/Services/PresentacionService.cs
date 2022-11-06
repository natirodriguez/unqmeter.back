using UnqMeterAPI.Interfaces;
using AutoMapper;
using UnqMeterAPI.DTO;
using UnqMeterAPI.Models;
using System.Collections.ObjectModel;
using UnqMeterAPI.Enums;
using System.Linq;

namespace UnqMeterAPI.Services
{
    public class PresentacionService : IPresentacionService
    {
        private readonly IMapper _mapper;
        private IRepositoryManager<Presentacion> _presentacionRepository;
        private IRepositoryManager<Slyde> _slydeRepository;
        private IRepositoryManager<OpcionesSlyde> _opcionesSlydeRepository;

        public PresentacionService(IMapper mapper, IRepositoryManager<Presentacion> presentacionRepository, IRepositoryManager<Slyde> slydeRepository,
            IRepositoryManager<OpcionesSlyde> opcionesSlydeRepository)
        {
            _mapper = mapper;
            _presentacionRepository = presentacionRepository;
            _slydeRepository = slydeRepository;
            _opcionesSlydeRepository = opcionesSlydeRepository;
        }

        public IList<PresentacionDTO> GetMisPresentaciones(string email)
        {
            var presentacionRepo = _presentacionRepository.GetAll().ToList();
            var presentaciones = presentacionRepo.Where(x => x.UsuarioCreador == email).ToList();
            IList<PresentacionDTO> presentacionesDTO = new List<PresentacionDTO>();

            if (presentaciones.Count > 0)
                presentacionesDTO = presentaciones.Select(x => new PresentacionDTO(x.Id, x.Nombre, x.FechaCreacion.ToString("dd/MM/yyyy"), x.UsuarioCreador, x.TiempoDeVida, 
                    (int)x.TipoTiempoDeVida, x.TipoTiempoDeVida.GetEnumDescription(), x.FechaInicioPresentacion, x.FechaFinPresentacion)).ToList();

            return presentacionesDTO;
        }

        public PresentacionDTO GetPresentacion(int id)
        {
            Presentacion? presentacion = _presentacionRepository.FindBy(x => x.Id == id).FirstOrDefault();
            PresentacionDTO presentacionDTO = new PresentacionDTO();

            if (presentacion != null)
            {
                presentacionDTO.id = presentacion.Id;
                presentacionDTO.nombre = presentacion.Nombre;
                presentacionDTO.fechaCreacion = presentacion.FechaCreacion.ToString("dd/MM/yyyy");
                presentacionDTO.usuarioCreador = presentacion.UsuarioCreador;
                presentacionDTO.tiempoDeVida = presentacion.TiempoDeVida;
                presentacionDTO.tipoTiempoDeVida = (int)presentacion.TipoTiempoDeVida;
                presentacionDTO.tipoTiempoDeVidaDescripcion = presentacion.TipoTiempoDeVida.GetEnumDescription();
            }
            return presentacionDTO;
        }

        public Presentacion CrearNuevaPresentacion(PresentacionDTO presentacionDTO)
        {
            Presentacion presentacion = new Presentacion();
            presentacion.Nombre = presentacionDTO.nombre;
            presentacion.UsuarioCreador = presentacionDTO.usuarioCreador;
            presentacion.FechaCreacion = DateTime.Now;
            presentacion.TiempoDeVida = presentacionDTO.tiempoDeVida;
            presentacion.TipoTiempoDeVida = (TipoTiempoDeVida)presentacionDTO.tipoTiempoDeVida;

            _presentacionRepository.Add(presentacion);
            _presentacionRepository.Save();

            CrearNuevaSlyde(presentacion, TipoPregunta.WORK_CLOUD);

            return presentacion;
        }

        public Slyde CrearNuevaSlyde(Presentacion presentacion, TipoPregunta? questionType)
        {
            Slyde slyde = new Slyde()
            {
                TipoPregunta = questionType,
                FechaCreacion = DateTime.Now,
                Presentacion = presentacion
            };

            _slydeRepository.Add(slyde);
            _slydeRepository.Save();

            return slyde;
        }
        public Slyde EditarSlyde(Presentacion presentacion, SlydeDTO slydeDto)
        {
            Slyde slyde = new Slyde()
            {
                Id = slydeDto.Id,
                TipoPregunta = (TipoPregunta?)slydeDto.TipoPregunta,
                FechaCreacion = slydeDto.FechaCreacion,
                Presentacion = presentacion,
                PreguntaRealizada = slydeDto.PreguntaRealizada,
                CantMaxRespuestaParticipantes = slydeDto.CantMaxRespuestaParticipantes
            };

            _slydeRepository.Edit(slyde);
            _slydeRepository.Save();

            SaveOptions(slydeDto.OpcionesSlydes,slyde);

            return slyde;
        }

        public Slyde? EliminarSlyde(int slydeId)
        {
            Slyde? slydeToDelete = _slydeRepository.FindBy(x => x.Id == slydeId).FirstOrDefault();

            if(slydeToDelete != null)
            {
                _slydeRepository.Delete(slydeToDelete);
                _slydeRepository.Save();
            }

            return slydeToDelete;
        }

        public Presentacion? EliminarPresentacion(int idPresentacion)
        {
            Presentacion? presentacionAEliminar = _presentacionRepository.FindBy(x => x.Id == idPresentacion).FirstOrDefault();

            if (presentacionAEliminar != null)
            {
                List<Slyde> slydesAEliminar = GetSlydesByIdPresentacion(idPresentacion);

                foreach(Slyde slyde in slydesAEliminar)
                {
                    EliminarSlyde(slyde.Id);
                }

                _presentacionRepository.Delete(presentacionAEliminar);
                _presentacionRepository.Save();
            }

            return presentacionAEliminar;
        }

        public List<Slyde> GetSlydesByIdPresentacion(long idPresentacion)
        {
            List<Slyde> slydes = _slydeRepository.FindBy(x => x.Presentacion.Id == idPresentacion).ToList();
            foreach(Slyde slyde in slydes)
            {
                slyde.OpcionesSlydes = GetOpcionesByIdSlyde(slyde.Id);
            }

            return slydes;
        }

        public List<OpcionesSlyde> GetOpcionesByIdSlyde(int idSlide)
        {
            return _opcionesSlydeRepository.FindBy(x => x.Slyde.Id == idSlide).ToList();
        }

        public void ClonarPresentacion(long id)
        {
            Presentacion? presentacion = _presentacionRepository.FindBy(x => x.Id == id).FirstOrDefault();
            if (presentacion != null)
            {
                Presentacion presentacionClone = presentacion.Clone();

                _presentacionRepository.Add(presentacionClone);
                _presentacionRepository.Save();

                foreach (Slyde slyde in GetSlydesByIdPresentacion(id))
                {
                    Slyde slydeClone = slyde.Clone(presentacionClone);

                    _slydeRepository.Add(slydeClone);
                    _slydeRepository.Save();
                }
            }
        }

        public Presentacion CompartirPresentacion(int id)
        {
            Presentacion presentacion = GetPresentationModel(id);

            if(presentacion.FechaInicioPresentacion == null)
            {
                presentacion.FechaInicioPresentacion = DateTime.Now;

                switch (presentacion.TipoTiempoDeVida)
                {
                    case TipoTiempoDeVida.DIA:
                        presentacion.FechaFinPresentacion = presentacion.FechaInicioPresentacion.Value.AddDays(presentacion.TiempoDeVida);
                        break;
                    default:
                        presentacion.FechaFinPresentacion = presentacion.FechaInicioPresentacion.Value.AddHours(presentacion.TiempoDeVida);
                        break;
                }
            }

            _presentacionRepository.Save();

            return presentacion;
        }

        public bool EstaVencidaLaPresentacion(int id)
        {
            Presentacion presentacion = GetPresentationModel(id);

            return presentacion.FechaFinPresentacion != null && DateTime.Now > presentacion.FechaFinPresentacion;
        }

        public IList<TipoPreguntaDTO> GetTipoPreguntas()
        {
            var tipos = new Collection<TipoPreguntaDTO>();

            tipos.Add(new TipoPreguntaDTO() { Descripcion = TipoPregunta.MULTIPLE_CHOICE.GetEnumDescription(), Codigo = TipoPregunta.MULTIPLE_CHOICE});
            tipos.Add(new TipoPreguntaDTO() { Descripcion = TipoPregunta.WORK_CLOUD.GetEnumDescription(), Codigo = TipoPregunta.WORK_CLOUD });
            tipos.Add(new TipoPreguntaDTO() { Descripcion = TipoPregunta.RANKING.GetEnumDescription(), Codigo = TipoPregunta.RANKING });
            tipos.Add(new TipoPreguntaDTO() { Descripcion = TipoPregunta.TEXTO_ABIERTO.GetEnumDescription(), Codigo = TipoPregunta.TEXTO_ABIERTO });

            return tipos;
        }

        public Presentacion GetPresentationModel(int id)
        {
            return _presentacionRepository.FindBy(x => x.Id == id).First();
        }

        public void SaveOptions(IList<OpcionesSlydeDTO> optionsSlydes, Slyde slyde)
        {
            foreach (var option in optionsSlydes)
            {
                OpcionesSlyde optionNew = new OpcionesSlyde();
                optionNew.Id = option.Id;
                optionNew.Slyde = slyde;
                optionNew.Opcion = option.Opcion;

                if (option.Id == 0)
                {
                    _opcionesSlydeRepository.Add(optionNew);
                }
                else
                {
                    _opcionesSlydeRepository.Edit(optionNew);
                }

                _opcionesSlydeRepository.Save();

            }
        }

        public OpcionesSlyde? DeleteOptionSlyde(int optionSlydeId)
        {
            OpcionesSlyde? optionSlydeToDelete = _opcionesSlydeRepository.FindBy(x => x.Id == optionSlydeId).FirstOrDefault();

            if (optionSlydeToDelete != null)
            {
                _opcionesSlydeRepository.Delete(optionSlydeToDelete);
                _opcionesSlydeRepository.Save();
            }

            return optionSlydeToDelete;
        }
    }
}
