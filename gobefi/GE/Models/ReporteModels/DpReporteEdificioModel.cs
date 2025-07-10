using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.ReporteModels
{
    public class DpReporteEdificioModel
    {
        public int Id { get; set; }
        public string Servicio { get; set; }
        public string Direccion { get; set; }
        public string Region { get; set; }
        public string Comuna { get; set; }
        public string DpP1 { get; set; }
        public string DpP2 { get; set; }
        public string DpP3 { get; set; }
        public string DpP4 { get; set; }

    }
}
