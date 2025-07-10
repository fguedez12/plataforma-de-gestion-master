using GobEfi.Web.Models.AislacionesModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EstructuraModels
{
    public class EstructuraAislacionModel
    {
        public long EstructuraId { get; set; }
        public long AislacionId { get; set; }
        public EstructuraModel Estructura { get; set; }
        public AislacionModel Aislacion { get; set; }
    }
}
