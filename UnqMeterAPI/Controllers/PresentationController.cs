using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UnqMeterAPI.DTO;
using UnqMeterAPI.Enums;
using UnqMeterAPI.Interfaces;
using UnqMeterAPI.Models;
using UnqMeterAPI.Services;

namespace UnqMeterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresentationController : ControllerBase
    {
        private readonly ILogger<PresentationController> _logger;
        private IPresentacionService _presentacionService;

        public PresentationController(IMapper mapper, ILogger<PresentationController> logger, IRepositoryManager<Presentacion> presentacionRepository, IRepositoryManager<Slyde> slydeRepository, 
            IRepositoryManager<OpcionesSlyde> opcionesSlydeRepository)
        {
            _logger = logger;
            _presentacionService = new PresentacionService(mapper, presentacionRepository, slydeRepository, opcionesSlydeRepository);
        }

        [HttpGet("GetMisPresentaciones/{email}")]
        public IActionResult GetMisPresentaciones(string email)
        {
            IList<PresentacionDTO> presentacionesDTO = _presentacionService.GetMisPresentaciones(email); 

            return Ok(presentacionesDTO);
        }

        [HttpGet("GetPresentacion/{id}")]
        public IActionResult GetPresentacion(int id)
        {
            PresentacionDTO presentacionDTO = _presentacionService.GetPresentacion(id);

            return Ok(presentacionDTO);
        }

        [HttpGet("GetTipoPreguntas")]
        public IActionResult GetTipoPreguntas()
        {
            IList<TipoPreguntaDTO> tipos = _presentacionService.GetTipoPreguntas();

            return Ok(tipos);
        }

        [HttpPost("PostNuevaPresentacion")]
        public IActionResult PostNuevaPresentacion([FromBody] PresentacionDTO presentacionDTO)
        {
            try
            {
                Presentacion presentacion = _presentacionService.CrearNuevaPresentacion(presentacionDTO);
                return Ok(presentacion);
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }

        [HttpPost("PostClonarPresentacion")]
        public IActionResult ClonarPresentacion([FromBody] int id)
        {
            try
            {
                _presentacionService.ClonarPresentacion(id);

                return Ok();
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }
    }
}