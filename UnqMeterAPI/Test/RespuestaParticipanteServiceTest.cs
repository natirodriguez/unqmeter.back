using AutoMapper;
using Moq;
using NUnit.Framework;
using UnqMeterAPI.Interfaces;
using UnqMeterAPI.Models;
using UnqMeterAPI.Services;
namespace UnqMeterAPI.Test
{
    [TestFixture]
    public class RespuestaParticipanteServiceTest
    {
        private Mock<IMapper> _mockMapper;
        private IRespuestaParticipanteService _respuestaParticipanteService;
        private Mock<IRepositoryManager<Respuesta>> _repositoryRespuestaMocker;
        IList<Respuesta> respuestas;

        [SetUp]
        public void SetUp()
        {
            _mockMapper = new Mock<IMapper>();
            _repositoryRespuestaMocker = new Mock<IRepositoryManager<Respuesta>>();

            _respuestaParticipanteService = new RespuestaParticipanteService(_mockMapper.Object, _repositoryRespuestaMocker.Object);

            respuestas = new List<Respuesta>();
        }
  
    }
}
