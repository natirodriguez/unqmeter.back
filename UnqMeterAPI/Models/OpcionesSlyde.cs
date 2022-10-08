namespace UnqMeterAPI.Models
{
    public class OpcionesSlyde
    {
        public int Id { get; set; }
        public Slyde Slyde { get; set; }
        public string Opcion { get; set; }

        public OpcionesSlyde Clone(Slyde slydeCopy)
        {
            OpcionesSlyde opcionSlydeCopy = (OpcionesSlyde)this.MemberwiseClone();
            opcionSlydeCopy.Id = 0;
            opcionSlydeCopy.Slyde = slydeCopy;

            return opcionSlydeCopy;
        }
    }
}
