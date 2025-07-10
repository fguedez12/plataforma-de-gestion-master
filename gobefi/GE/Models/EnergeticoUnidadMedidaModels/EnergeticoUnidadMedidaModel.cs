using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Models.UnidadMedidaModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EnergeticoUnidadMedidaModels
{
    public class EnergeticoUnidadMedidaModel : BaseModel<long>
    {
        public double Calor;
        public double Densidad;
        public double Factor;
        public long EnergeticoId;
        public long UnidadMedidaId;
        public EnergeticoModel Eneergetico;
        public UnidadMedidaModel UnidadMedida;
    }
}
