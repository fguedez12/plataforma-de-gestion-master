using GobEfi.Web.Models.TipoSombreadoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EstructuraModels
{
    public class EstructuraTipoSombreadoModel
    {
        public long EstructuraId { get; set; }
        public long TipoSombreadoId { get; set; }
        public EstructuraModel  Estructura { get; set; }
        public TipoSombreadoModel TipoSombreado { get; set; }
    }
}
