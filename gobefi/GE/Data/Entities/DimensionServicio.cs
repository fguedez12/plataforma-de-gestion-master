namespace GobEfi.Web.Data.Entities
{
    public class DimensionServicio
    {
        public long Id { get; set; }
        public long DimensionBrechaId { get; set; }
        public long ServicioId { get; set; }
        public bool IngresoObservacion { get; set; }
        public string Observacion { get; set; }
        public bool IngresoObservacionObjetivos { get; set; }
        public string ObservacionObjetivos { get; set; }
        public bool IngresoObservacionAcciones { get; set; }
        public string ObservacionAcciones { get; set; }
        public bool IngresoObservacionIndicadores { get; set; }
        public string ObservacionIndicadores { get; set; }
        public DimensionBrecha DimensionBrecha { get; set; }
        public Servicio Servicio { get; set; }
    }
}
