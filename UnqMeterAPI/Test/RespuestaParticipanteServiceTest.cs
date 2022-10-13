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
        private Mock<IRepositoryManager<Slyde>> _repositorySlydeMocker;

        [SetUp]
        public void SetUp()
        {
            _mockMapper = new Mock<IMapper>();
            _repositoryRespuestaMocker = new Mock<IRepositoryManager<Respuesta>>();
            _repositorySlydeMocker = new Mock<IRepositoryManager<Slyde>>();

            _respuestaParticipanteService = new RespuestaParticipanteService(_mockMapper.Object, _repositoryRespuestaMocker.Object, _repositorySlydeMocker.Object);
        }
  
        [Test]
        public void Test()
        {
            _repositorySlydeMocker.Setup(x => x.FindBy(x => x.Presentacion.Id == It.IsAny<int>())).Returns(GetSlydesPresentacion1().AsQueryable());
            _repositoryRespuestaMocker.Setup(x => x.GetAll()).Returns(GetRespuestas().AsQueryable());

            var slydesSinRespuesta = _respuestaParticipanteService.GetSlydesNoRespondidas(1, "1.1.1.1");

            Assert.AreEqual(2, slydesSinRespuesta.Count);
            Assert.AreEqual(2, slydesSinRespuesta[0].Id);
            Assert.AreEqual(3, slydesSinRespuesta[0].Id);
        }

        [Test]
        public void Test2()
        {
            _repositorySlydeMocker.Setup(x => x.FindBy(x => x.Presentacion.Id == It.IsAny<int>())).Returns(GetSlydesPresentacion2().AsQueryable());
            _repositoryRespuestaMocker.Setup(x => x.GetAll()).Returns(GetRespuestas().AsQueryable());

            var slydesSinRespuesta = _respuestaParticipanteService.GetSlydesNoRespondidas(2, "1.1.1.1");

            Assert.AreEqual(0, slydesSinRespuesta.Count);
        }

        private List<Slyde> GetSlydesPresentacion1()
        {
            Presentacion presentacion = new Presentacion() { Id = 1 };

            List<Slyde> slydes = new List<Slyde>();
            slydes.Add(new Slyde() { Presentacion = presentacion, Id = 1 });
            slydes.Add(new Slyde() { Presentacion = presentacion, Id = 2 });
            slydes.Add(new Slyde() { Presentacion = presentacion, Id = 3 });

            return slydes;
        }

        private List<Slyde> GetSlydesPresentacion2()
        {
            Presentacion presentacion = new Presentacion() { Id = 2 };

            List<Slyde> slydes = new List<Slyde>();
            slydes.Add(new Slyde() { Presentacion = presentacion, Id = 4 });

            return slydes; 
        }

        private List<Respuesta> GetRespuestas()
        {
            List<Respuesta> respuestas = new List<Respuesta>();

            respuestas.Add(new Respuesta() { Id = 1, Slyde = new Slyde() { Id = 1 }, Participante= "1.1.1.1" });
            respuestas.Add(new Respuesta() { Id = 2, Slyde = new Slyde() { Id = 4 }, Participante = "1.1.1.1" });

            return respuestas; 
        }
    }
}
