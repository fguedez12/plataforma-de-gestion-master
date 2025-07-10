using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Data.Entities
{
    public class UnidadMedida
    {
        public long Id { get;set;}
        public string Nombre { get;set; }
        public string Abrv { get;set; }

        public ICollection<EnergeticoUnidadMedida> EnergeticoUnidadMedidas { get; set; }
        public ICollection<ParametroMedicion> ParametrosMedicion { get; set; }
    }
}
