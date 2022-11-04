using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UnqMeterAPI.DTO;
using UnqMeterAPI.Interfaces;
using UnqMeterAPI.Models;
using UnqMeterAPI.Services;

namespace UnqMeterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RespuestaParticipanteController : ControllerBase
    {
        private readonly ILogger<RespuestaParticipanteController> _logger;
        private IRespuestaParticipanteService _respuestaParticipanteService;
        public RespuestaParticipanteController(IMapper mapper, ILogger<RespuestaParticipanteController> logger, IRepositoryManager<Respuesta> respuestaRepository, IRepositoryManager<Slyde> slydeRepository, IRepositoryManager<DescripcionRespuesta> descripcionRespuestaRepository)
        {
            _logger = logger;
            _respuestaParticipanteService = new RespuestaParticipanteService(mapper, respuestaRepository, slydeRepository, descripcionRespuestaRepository);
        }

        [HttpGet("GetSlydesSinRespuestas/{idPresentacion}/{ip}")]
        public IActionResult GetSlydesSinRespuestas(int idPresentacion, string ip)
        {
            IList<Slyde> slydes = _respuestaParticipanteService.GetSlydesSinRespuestas(idPresentacion, ip);

            return Ok(slydes);
        }

        [HttpPost("SaveRespuesta")]
        public IActionResult SaveRespuesta([FromBody] RespuestaDTO respuestaDTO)
        {
            try
            {
                var respuesta = _respuestaParticipanteService.SaveRespuesta(respuestaDTO);

                return Ok(respuesta);
            }
            catch (Exception e)
            {
                throw new Exception(e.StackTrace);
            }
        }
    }
}
