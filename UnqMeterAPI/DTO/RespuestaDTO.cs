namespace UnqMeterAPI.DTO
{
    public class RespuestaDTO
    {
        public int Id { get; set; }
        public int SlydeId { get; set; }
        public string Participante { get; set; }
        public string FechaCreacion { get; set; }
        public string Descripcion { get; set; }
        public int OpcionElegidaId { get; set; }
    }
}
