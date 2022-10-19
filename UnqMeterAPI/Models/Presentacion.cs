using System.ComponentModel;

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

        public Presentacion Clone()
        {
            Presentacion presentacionCopia = (Presentacion)this.MemberwiseClone();
            presentacionCopia.Id = 0;
            presentacionCopia.Nombre += " Copy";
            presentacionCopia.FechaCreacion = DateTime.Now;
            presentacionCopia.FechaInicioPresentacion = null;
            presentacionCopia.FechaFinPresentacion = null;

            return presentacionCopia;
        }
    }

    public enum TipoTiempoDeVida
    {
        [Description("Horas")] 
        HORA = 1,  
        [Description("Dias")]
        DIA = 2
    }
}
