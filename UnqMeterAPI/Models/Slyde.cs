namespace UnqMeterAPI.Models
{
    public class Slyde
    {
        public int Id { get; set; }
        public int PresentacionId { get; set; }
        public string PreguntaRealizada { get; set; }
        public TipoPregunta? TipoPregunta { get; set; }
        public DateTime FechaCreacion { get; set; }
    }

    public enum TipoPregunta
    {
        INDEFINIDO = 0,
        MUTIPLE_CHOISE = 1
    }
}
