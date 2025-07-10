using GobEfi.Web.Models.AreaModels;
using GobEfi.Web.Models.MuroModels;
using GobEfi.Web.Models.NumeroPisoModels;
using GobEfi.Web.Models.SueloModels;
using GobEfi.Web.Models.TipoNivelPisoModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.PisoModels
{
    public class PisoModel
    {

        public long Id { get; set; }
        public double Superficie { get; set; }
        public double Altura { get; set; }
        public long NumeroPisoId { get; set; }
        public long DivisionId { get; set; }
        public bool Active { get; set; }
        public int PisoNumero { get; set; }
        public string NumeroPisoNombre { get; set; }
        public int TipoNivelId { get; set; }
        public string Piso { get; set; }
        public long PisoId { get; set; }
        public int FrontisIndex { get; set; }
        public long TipoUsoId { get; set; }
        public string NroRol { get; set; }
        public List<MuroModel> Muros { get; set; }
        public List<SueloModel> Suelos { get; set; }
        public List<AreaModel> Areas { get; set; }
        public List<UnidadPisoModel> UnidadesPisos { get; set; }

    }
}
