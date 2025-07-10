using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.PisoModels
{
    public class PisoPasoUnoModel
    {
        public long Id { get; set; }
        public int PisoNumero { get; set; }
        public string NumeroPisoNombre { get; set; }
        public int NumeroPisoId { get; set; }
        public decimal Superficie { get; set; }
        public decimal Altura { get; set; }
        public int TipoNivelId { get; set; }
        public string NroRol { get; set; }
        public int TipoUsoId { get; set; }
    }
}
