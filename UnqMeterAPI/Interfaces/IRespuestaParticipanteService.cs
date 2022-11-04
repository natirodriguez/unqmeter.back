using UnqMeterAPI.DTO;
using UnqMeterAPI.Models;

namespace UnqMeterAPI.Interfaces
{
    public interface IRespuestaParticipanteService
    {
        IList<Slyde> GetSlydesSinRespuestas(int idPresentacion, string ipUsuario);
        Respuesta SaveRespuesta(RespuestaDTO respuestaDTO);
    }
}
