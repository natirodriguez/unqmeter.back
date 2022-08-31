using Microsoft.AspNetCore.Mvc;

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
            var message = new Message();
            message.content = "Hello World";
            
            return Ok(message);
        }
    }
}