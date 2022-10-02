namespace UnqMeterAPI.DTO
{
    public class SlydeDTO
    {
        public int Id { get; set; }
        public int PresentacionDTO { get; set; }
        public string PreguntaRealizada { get; set; }
        public int? TipoPregunta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? CantMaxRespuestaParticipantes { get; set; }
    }
}
