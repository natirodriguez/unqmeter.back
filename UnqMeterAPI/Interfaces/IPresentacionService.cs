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
        Presentacion CompartirPresentacion(int id);
        List<Slyde> GetSlydesByIdPresentacion(long idPresentacion);
        Slyde CrearNuevaSlyde(Presentacion presentacion, TipoPregunta? questionType);
        Slyde EditarSlyde(Presentacion presentacion,SlydeDTO slyde);
        Slyde? EliminarSlyde(int slydeId);
        bool EstaVencidaLaPresentacion(int id);
        Presentacion? EliminarPresentacion(int idPresentacion);
        OpcionesSlyde? DeleteOptionSlyde(int optionSlydeId);
        
    }
}
