using UnqMeterAPI.Interfaces;
using AutoMapper;
using UnqMeterAPI.DTO;
using UnqMeterAPI.Models;

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
                presentacionesDTO = presentaciones.Select(x => new PresentacionDTO() {id = x.Id, nombre = x.Nombre, fechaCreacion = x.FechaCreacion.ToString("dd/MM/yyyy"), usuarioCreador = x.UsuarioCreador }).ToList();

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

            Slyde slyde = new Slyde()
            {
                TipoPregunta = TipoPregunta.INDEFINIDO,
                FechaCreacion = DateTime.Now,
                PresentacionId = presentacion.Id
            };

            _slydeRepository.Add(slyde);
            _slydeRepository.Save();

            return presentacion;
        }
    }
}
