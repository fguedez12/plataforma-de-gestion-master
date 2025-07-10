using GobEfi.Web.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services.Request
{
    public class EmpresaDistribuidoraRequest : BaseRequest
    {
        public string Nombre { get; set; }
        public long EnergeticoId { get; set; }
        public long RegionId { get; set; }
        public long ProvinciaId { get; set; }
        public long ComunaId { get; set; }
        public bool Activo { get; set; }
    }
}
