using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UnqMeterAPI.DTO;
using UnqMeterAPI.Interfaces;
using UnqMeterAPI.Models;

namespace UnqMeterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresentationController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILogger<PresentationController> _logger;
        private IRepositoryManager<Presentacion> _presentacionRepository;

        public PresentationController(IMapper mapper, ILogger<PresentationController> logger, IRepositoryManager<Presentacion> presentacionRepository)
        {
            _mapper = mapper;
            _logger = logger;
            _presentacionRepository = presentacionRepository;  
        }

        [HttpGet("GetMisPresentaciones/{email}")]
        public IActionResult GetMisPresentaciones(string email)
        {
            var presentacionRepo = _presentacionRepository.GetAll();
            var presentaciones = presentacionRepo.Where(x => x.UsuarioCreador == email).ToList();
            IList<PresentacionDTO> presentacionesDTO = new List<PresentacionDTO>(); 

            if (presentaciones.Count > 0)
                presentacionesDTO = presentaciones.Select(x => new PresentacionDTO() { Nombre = x.Nombre, UsuarioCreador = x.UsuarioCreador}).ToList();

            return Ok(presentacionesDTO);
        }

        [HttpPost("PostNuevaPresentacion")]
        public IActionResult PostNuevaPresentacion([FromBody] ExternalAuth externalAuth)
        {
            Presentacion presentacion = new Presentacion();
            return Ok(presentacion);

        }
    }
}