using AutoMapper;
using Moq;
using NUnit.Framework;
using UnqMeterAPI.Interfaces;
using UnqMeterAPI.Models;
using UnqMeterAPI.Services;

namespace UnqMeterAPI.Test
{
    [TestFixture]
    public class PresentacionServiceTest
    {
        private IPresentacionService _presentacionService;
        private Mock<IMapper> _mockMapper;
        private Mock<IRepositoryManager<Presentacion>> _repositoryPresentacionMocker;
        private Mock<IRepositoryManager<Slyde>> _repositorySlydeMocker;
        private Mock<IRepositoryManager<OpcionesSlyde>> _repositoryOpcionesSlydeMocker;

        [SetUp]
        public void SetUp()
        {
            _mockMapper = new Mock<IMapper>();
            _repositoryPresentacionMocker = new Mock<IRepositoryManager<Presentacion>>();
            _repositorySlydeMocker = new Mock<IRepositoryManager<Slyde>>();
            _repositoryOpcionesSlydeMocker = new Mock<IRepositoryManager<OpcionesSlyde>>();

            _presentacionService = new PresentacionService(_mockMapper.Object, _repositoryPresentacionMocker.Object, _repositorySlydeMocker.Object, _repositoryOpcionesSlydeMocker.Object);
        }

        [Test]
        public void GetMisPresentaciones_ListaVacia()
        {
            IList<Presentacion> presentaciones = new List<Presentacion>(); 
            _repositoryPresentacionMocker.Setup(x => x.GetAll()).Returns(presentaciones.AsQueryable());
            var result = _presentacionService.GetMisPresentaciones("nrodriguez@mail.com");

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetMisPresentaciones_NoEncuentraDatos()
        {
            IList<Presentacion> presentaciones = new List<Presentacion>() { new Presentacion() { UsuarioCreador= "nataliarodriguez@mail.com"} };
            _repositoryPresentacionMocker.Setup(x => x.GetAll()).Returns(presentaciones.AsQueryable());
            var result = _presentacionService.GetMisPresentaciones("nrodriguez@mail.com");

            Assert.AreEqual(0, result.Count);
        }


        [Test]
        public void GetMisPresentaciones_EncuentraDatos()
        {
            IList<Presentacion> presentaciones = new List<Presentacion>() { new Presentacion() { UsuarioCreador = "nrodriguez@mail.com", Nombre="Presentacion test" } };
            _repositoryPresentacionMocker.Setup(x => x.GetAll()).Returns(presentaciones.AsQueryable());
            var result = _presentacionService.GetMisPresentaciones("nrodriguez@mail.com");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Presentacion test", result.First().nombre);
        }
    }
}
