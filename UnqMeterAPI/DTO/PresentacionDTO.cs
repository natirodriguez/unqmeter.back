namespace UnqMeterAPI.DTO
{
    public class PresentacionDTO
    {
        public int id { get; set; }
        public string? nombre { get; set; }
        public string? usuarioCreador { get; set; }
        public string? fechaCreacion { get; set; }
        public int tiempoDeVida { get; set; }
        public int tipoTiempoDeVida { get; set; }
        public DateTime? fechaInicioPresentacion { get; set; }
        public DateTime? fechaFinPresentacion { get; set; }
    }
}
