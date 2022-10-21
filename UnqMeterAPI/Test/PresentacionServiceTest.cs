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

        IList<Presentacion> presentaciones;
        string USUARIO_CREADOR = "practicas.des.soft@mail.com"; 

        [SetUp]
        public void SetUp()
        {
            _mockMapper = new Mock<IMapper>();
            _repositoryPresentacionMocker = new Mock<IRepositoryManager<Presentacion>>();
            _repositorySlydeMocker = new Mock<IRepositoryManager<Slyde>>();
            _repositoryOpcionesSlydeMocker = new Mock<IRepositoryManager<OpcionesSlyde>>();

            _presentacionService = new PresentacionService(_mockMapper.Object, _repositoryPresentacionMocker.Object, _repositorySlydeMocker.Object, _repositoryOpcionesSlydeMocker.Object);

            presentaciones = new List<Presentacion>();
        }

        [Test]
        public void GetMisPresentaciones_NoEncuentraDatos()
        {
            presentaciones.Add(GetPresentacion());
            _repositoryPresentacionMocker.Setup(x => x.GetAll()).Returns(presentaciones.AsQueryable());
            var result = _presentacionService.GetMisPresentaciones("nrodriguez@mail.com");

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void GetMisPresentaciones_EncuentraDatos()
        {
            presentaciones.Add(GetPresentacion());
            _repositoryPresentacionMocker.Setup(x => x.GetAll()).Returns(presentaciones.AsQueryable());
            var result = _presentacionService.GetMisPresentaciones(USUARIO_CREADOR);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("Presentacion test", result.First().nombre);
        }

        [Test]
        public void SinFechaSeteada_CompartirPresentacion_SeteaCorrectamenteFechaInicioFin()
        {
            presentaciones.Add(GetPresentacion());
            _repositoryPresentacionMocker.Setup(x => x.FindBy(x => x.Id == 1)).Returns(presentaciones.AsQueryable());
            var result = _presentacionService.CompartirPresentacion(1);

            Assert.IsNotNull(result.FechaInicioPresentacion);
            Assert.IsNotNull(result.FechaFinPresentacion);
            Assert.AreEqual(result.FechaInicioPresentacion.Value.AddDays(1), result.FechaFinPresentacion.Value);
        }

        [Test]
        public void ConFechaSeteada_CompartirPresentacion_NoActualizaFecha()
        {
            Presentacion presentacion = GetPresentacionConFechas(-2, -1);
            presentaciones.Add(presentacion);
            _repositoryPresentacionMocker.Setup(x => x.FindBy(x => x.Id == 1)).Returns(presentaciones.AsQueryable());
            var result = _presentacionService.CompartirPresentacion(1);

            Assert.AreEqual(presentacion.FechaInicioPresentacion, result.FechaInicioPresentacion);
            Assert.AreEqual(presentacion.FechaFinPresentacion, result.FechaFinPresentacion);
        }

        [Test]
        public void FechaFinVencida_EstaVencidaLaPresentacion_True()
        {
            presentaciones.Add(GetPresentacionConFechas(-2,-1));
            _repositoryPresentacionMocker.Setup(x => x.FindBy(x => x.Id == 1)).Returns(presentaciones.AsQueryable());
            var result = _presentacionService.EstaVencidaLaPresentacion(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void FechaFinVencida_EstaVencidaLaPresentacion_False()
        {
            presentaciones.Add(GetPresentacionConFechas(-1, 1));
            _repositoryPresentacionMocker.Setup(x => x.FindBy(x => x.Id == 1)).Returns(presentaciones.AsQueryable());
            var result = _presentacionService.EstaVencidaLaPresentacion(1);

            Assert.IsFalse(result);
        }

        private Presentacion GetPresentacion()
        {
            return new Presentacion() { UsuarioCreador = USUARIO_CREADOR, Nombre = "Presentacion test", TiempoDeVida=1, TipoTiempoDeVida = TipoTiempoDeVida.DIA };
        }

        private Presentacion GetPresentacionConFechas(int cantDiasInicio, int cantDiasFin)
        {
            Presentacion presentacion = GetPresentacion();
            presentacion.FechaInicioPresentacion = DateTime.Now.AddDays(cantDiasInicio);
            presentacion.FechaFinPresentacion = DateTime.Now.AddDays(cantDiasFin);

            return presentacion; 
        }
    }
}
