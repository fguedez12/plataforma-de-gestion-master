using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.PisoModels
{
    public class PisoForSaveModel
    {
        public long TipoNivelId { get; set; }
        public long NumeroPisoId { get; set; }
        public bool EntrePiso { get; set; }
        public double Superficie { get; set; }
        public double Altura { get; set; }
        public long? DivisionId { get; set; }
        public long? EdificioId { get; set; }
        public decimal? TipoUsoId { get; set; }
        public bool PisosIguales { get; set; }
        public long? InmuebleId { get; set; }
        public string NroRol { get; set; }
    }
}
