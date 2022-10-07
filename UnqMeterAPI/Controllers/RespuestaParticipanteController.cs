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

        public RespuestaParticipanteController(IMapper mapper, ILogger<RespuestaParticipanteController> logger)
        {
            _logger = logger;
        }
    }
}
