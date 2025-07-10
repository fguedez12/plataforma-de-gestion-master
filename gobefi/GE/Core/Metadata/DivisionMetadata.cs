using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Metadata
{
    public class DivisionMetadata
    {
        [Display(Name = "Nombre")]
        public string Nombre { set; get; }
        [Display(Name = "Funcionarios")]
        public int Funcionarios { get; set; }
        [Display(Name ="Reporta PMG")]
        public bool ReportaPMG { get; set; }
        [Display(Name = "Año de Construcción")]
        public int AnyoConstruccion { get; set; }
        [Display(Name = "Latitud")]
        public double Latitud { get; set; }
        [Display(Name = "Longitud")]
        public double Longitud { get; set; }
    }
}
