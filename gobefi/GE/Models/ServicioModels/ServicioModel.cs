using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Models.InstitucionModels;

namespace GobEfi.Web.Models.ServicioModels
{
    public class ServicioModel : BaseModel<long>
    {
        public string Nombre { get; set; }

        public string Identificador { get; set; }
        
        [DisplayName("Institucion")]
        public long InstitucionId { get; set;}
        public bool ReportaPMG { get; set; }
        public bool CompraActiva { get; set; }
        [Display(Name = "OTCO del PGA – Etapa 2")]
        public bool? PgaRevisionRed { get; set; }
        [Display(Name = "Alcance Diagnostico")]
        public bool RevisionRed { get; set; }
        [Display(Name = "Validación Concientización")]
        public bool? ValidacionConcientizacion { get; set; }

        public InstitucionModel Institucion { get; set; }
    }
}
