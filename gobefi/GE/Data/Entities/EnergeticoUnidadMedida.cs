using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class EnergeticoUnidadMedida : BaseEntity
    {
        public double Calor { get; set; }
        public double Densidad { get; set; }
        public double Factor { get; set; }
        public long EnergeticoId { get; set; }
        public long UnidadMedidaId { get; set; }
        public Energetico Energetico { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
    }
}
