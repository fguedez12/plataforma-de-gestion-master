using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class Energetico : BaseEntity
    {
        public string Nombre { get; set; }
        public bool Multiple { get; set; }
        public string Icono { get; set; }
        public bool PermiteMedidor { get; set; }
        public int Posicion { get; set; }
        public bool PermiteTipoTarifa { get; set; }
        public bool PermitePotenciaSuministrada { get; set; }
        public long? OldId { get; set; }

        public virtual ICollection<EnergeticoDivision> EnergeticoDivision { get; set; }
        public virtual ICollection<EnergeticoUnidadMedida> EnergeticoUnMedida { get; set; }
        public virtual ICollection<Compra> Compras { get; set; }
        public List<TipoEquipoCalefaccionEnergetico> TipoCalefaccionEnergeticos { get; set; }
    }
}
