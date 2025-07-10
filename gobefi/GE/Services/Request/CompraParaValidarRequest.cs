using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services.Request
{
    public class CompraParaValidarRequest
    {
        public long? InstitucionId { get; set; }
        public long? ServicioId { get; set; }
        public long? RegionId { get; set; }
        public long? EdificioId { get; set; }
        public long? DivisionId { get; set; }
        public long? EnergeticoId { get; set; }
        public long? NumClienteId { get; set; }
        public long? NumMedidorId { get; set; }
        public long? CompraId { get; set; }
        public bool UnidadPmgId { get; set; }
        public string EstadoId { get; set; }
    }
}
