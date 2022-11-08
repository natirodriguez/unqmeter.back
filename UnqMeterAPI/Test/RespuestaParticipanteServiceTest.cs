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
        private IRespuestaParticipanteService _respuestaParticipanteService;
        private Mock<IRepositoryManager<Respuesta>> _repositoryRespuestaMocker;
        private Mock<IRepositoryManager<DescripcionRespuesta>> _repositoryDescripcionRespuestaMocker;
        private Mock<IRepositoryManager<Slyde>> _repositorySlydeMocker;

        const string participante = "1.1.1.1"; 

        [SetUp]
        public void SetUp()
        {
            _repositoryRespuestaMocker = new Mock<IRepositoryManager<Respuesta>>();
            _repositorySlydeMocker = new Mock<IRepositoryManager<Slyde>>();
            _repositoryDescripcionRespuestaMocker = new Mock<IRepositoryManager<DescripcionRespuesta>>();

            _respuestaParticipanteService = new RespuestaParticipanteService(_repositoryRespuestaMocker.Object, _repositorySlydeMocker.Object, _repositoryDescripcionRespuestaMocker.Object);
        }
  
        [Test]
        public void SlydeConYSinRespuesta_GetSlydesNoRespondidas_SlydesSinRespuesta()
        {
            _repositorySlydeMocker.Setup(x => x.FindBy(x => x.Presentacion.Id == 1)).Returns(GetSlydesPresentacion1().AsQueryable());
            _repositoryRespuestaMocker.Setup(x => x.FindBy(x => x.Participante == participante)).Returns(GetRespuestas().AsQueryable());

            var slydesSinRespuesta = _respuestaParticipanteService.GetSlydesSinRespuestas(1, participante);

            Assert.AreEqual(2, slydesSinRespuesta.Count);
            Assert.AreEqual(2, slydesSinRespuesta[0].Id);
            Assert.AreEqual(3, slydesSinRespuesta[1].Id);
        }

        [Test]
        public void SlydeConRespuesta_GetSlydesNoRespondidas_ListaVacia()
        {
            _repositorySlydeMocker.Setup(x => x.FindBy(x => x.Presentacion.Id == 2)).Returns(GetSlydesPresentacion2().AsQueryable());
            _repositoryRespuestaMocker.Setup(x => x.FindBy(x => x.Participante == participante)).Returns(GetRespuestas().AsQueryable());

            var slydesSinRespuesta = _respuestaParticipanteService.GetSlydesSinRespuestas(2, participante);

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
            slydes.Add(new Slyde() { Presentacion = presentacion, Id = 1 });

            return slydes; 
        }

        private List<Respuesta> GetRespuestas()
        {
            List<Respuesta> respuestas = new List<Respuesta>();

            respuestas.Add(new Respuesta() { Id = 1, Slyde = new Slyde() { Id = 1 }, Participante= "1.1.1.1" });

            return respuestas; 
        }
    }
}
