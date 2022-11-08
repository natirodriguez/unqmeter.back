namespace UnqMeterAPI.DTO
{
    public class RespuestaDTO
    {
        public int id { get; set; }
        public int slydeId { get; set; }
        public string participante { get; set; }
        public List<DescripcionRespuestaDTO> descripcionesRespuesta { get; set; }
        public string? descripcionGeneral { get; set; }
    }
}
