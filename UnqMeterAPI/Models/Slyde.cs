﻿using UnqMeterAPI.Enums;

namespace UnqMeterAPI.Models
{
    public class Slyde
    {
        public int Id { get; set; }
        public Presentacion Presentacion { get; set; }
        public string? PreguntaRealizada { get; set; }
        public TipoPregunta? TipoPregunta { get; set; }
        public DateTime FechaCreacion { get; set; }
        public int? CantMaxRespuestaParticipantes { get; set; }

        public Slyde Clone(Presentacion presentacion)
        {
            Slyde slydeCopy = (Slyde)this.MemberwiseClone();
            slydeCopy.Id = 0;
            slydeCopy.FechaCreacion = DateTime.Now;
            slydeCopy.Presentacion = presentacion;

            return slydeCopy;
        }
    }

}
