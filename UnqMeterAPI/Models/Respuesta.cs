namespace UnqMeterAPI.Models
{
    public class Respuesta
    {
        public int Id { get; set; }
        public Slyde Slyde { get; set; }
        public string Participante { get; set; }
        public DateTime FechaCreacion { get; set; }
        public string? DescripcionGeneral { get; set; }
        public OpcionesSlyde? OpcionElegida { get; set; }

    }
}
