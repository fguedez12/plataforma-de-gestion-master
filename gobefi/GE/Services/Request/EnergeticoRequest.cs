using GobEfi.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services.Request
{
    public class EnergeticoRequest : BaseRequest
    {
        public string Nombre { get; set; }
        public string Icono { get; set; }
        public bool Activo { get; set; }
    }
}
