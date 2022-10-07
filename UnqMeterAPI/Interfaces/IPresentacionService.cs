using UnqMeterAPI.DTO;
using UnqMeterAPI.Models;

namespace UnqMeterAPI.Interfaces
{
    public interface IPresentacionService
    {
        IList<PresentacionDTO> GetMisPresentaciones(string email);
        Presentacion CrearNuevaPresentacion(PresentacionDTO presentacionDTO);
        PresentacionDTO GetPresentacion(int id);

    }
}
