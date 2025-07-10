using GobEfi.Web.Models.MaterialidadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EstructuraModels
{
    public class EstructuraMaterialidadModel
    {
        public long EstructuraId { get; set; }
        public long MaterialidadId { get; set; }
        public EstructuraModel Estructura { get; set; }
        public MaterialidadModel Materialidad { get; set; }
    }
}
