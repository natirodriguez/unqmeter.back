using Microsoft.AspNetCore.Mvc;
using UnqMeterAPI.Models;

namespace UnqMeterAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PresentationController : ControllerBase
    {
       
        private readonly ILogger<PresentationController> _logger;

        public PresentationController(ILogger<PresentationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetHelloWorld")]
        public IActionResult GetHelloWorld()
        {
            var message = new Message
            {
                Content = "Hello World"
            };

            return Ok(message);
        }

        [HttpGet("GetMisPresentaciones")]
        public IActionResult GetMisPresentaciones()
        {
            var presentacion = new Presentacion();
            presentacion.nombre = "Presentacion 1";

            IList<Presentacion> presentaciones = new List<Presentacion>();
            presentaciones.Add(presentacion);

            return Ok(presentaciones);
        }
    }
}