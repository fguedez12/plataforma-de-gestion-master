using System.Collections.Generic;

namespace GobEfi.Web.Data.Entities
{
    public class TipoEquipoCalefaccion
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public float Rendimiento { get; set; }
        public float A { get; set; }
        public float B { get; set; }
        public float C { get; set; }
        public float Temp { get; set; }
        public float Costo { get; set; }
        public float Costo_Social { get; set; }
        public float Costo_Mant { get; set; }
        public float Costo_Social_Mant { get; set; }
        public float Ejec_HD_Maestro { get; set; }
        public float Ejec_HD_Ayte { get; set; }
        public float Ejec_HD_Jornal { get; set; }
        public float Mant_HD_Maestro { get; set; }
        public float Mant_HD_Ayte { get; set; }
        public float Mant_HD_Jornal { get; set; }
        public bool CA { get; set; }
        public bool FR { get; set; }
        public bool AC { get; set; }
        public List<TipoEquipoCalefaccionEnergetico> TipoCalefaccionEnergeticos { get; set; }
    }
}
