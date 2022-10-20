using UnqMeterAPI.Models;

namespace UnqMeterAPI.DTO
{
    public class PresentacionDTO
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? usuarioCreador { get; set; }
        public string? fechaCreacion { get; set; }
        public int tiempoDeVida { get; set; }
        public string tipoTiempoDeVidaDescripcion { get; set; }
        public int tipoTiempoDeVida { get; set; }
        public DateTime? fechaInicioPresentacion { get; set; }
        public DateTime? fechaFinPresentacion { get; set; }

        public PresentacionDTO(int _id, string? _nombre, string? _fechaCreacion, string? _usuarioCreador, int _tiempoDeVida, int _tipoTiempoDeVida, string _tipoTiempoDeVidaDescripcion)
        {
            id = _id;
            nombre = _nombre;
            fechaCreacion = _fechaCreacion;
            usuarioCreador = _usuarioCreador;
            tiempoDeVida = _tiempoDeVida;
            tipoTiempoDeVida = _tipoTiempoDeVida;
            tipoTiempoDeVidaDescripcion = _tipoTiempoDeVidaDescripcion; 
        }

        public PresentacionDTO()
        {

        }
    }
}
