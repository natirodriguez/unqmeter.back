using UnqMeterAPI.DTO;
using UnqMeterAPI.Enums;
using UnqMeterAPI.Models;

namespace UnqMeterAPI.Interfaces
{
    public interface IPresentacionService
    {
        IList<PresentacionDTO> GetMisPresentaciones(string email);
        Presentacion CrearNuevaPresentacion(PresentacionDTO presentacionDTO);
        PresentacionDTO GetPresentacion(int id);
        Presentacion GetPresentationModel(int id);
        void ClonarPresentacion(long id);
        IList<TipoPreguntaDTO> GetTipoPreguntas();
        List<Slyde> GetSlydesByIdPresentacion(long idPresentacion);
        Slyde CrearNuevaSlyde(Presentacion presentacion, TipoPregunta? questionType);

    }
}
