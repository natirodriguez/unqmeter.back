namespace UnqMeterAPI.Models
{
    public class Presentacion 
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public string? UsuarioCreador { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int TiempoDeVida { get; set; }
        public TipoTiempoDeVida TipoTiempoDeVida { get; set; }
        public DateTime? FechaInicioPresentacion{ get; set; }
        public DateTime? FechaFinPresentacion { get; set; }
    }

    public enum TipoTiempoDeVida
    {
        HORA = 1,  
        DIA = 2
    }
}
