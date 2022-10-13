using UnqMeterAPI.Models;

namespace UnqMeterAPI.Interfaces
{
    public interface IRespuestaParticipanteService
    {
        IList<Slyde> GetSlydesNoRespondidas(int idPresentacion, string ipUsuario);
    }
}
