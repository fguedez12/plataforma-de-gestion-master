using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Metadata
{
    public class EdificioMetadata
    {
        [Display(Name = "Dirección")]
        public string Direccion { get; set; }
        [Display(Name = "Nº")]
        public string Numero { get; set; }
        [Display(Name = "Calle")]
        public string Calle { get; set; }
        [Display(Name = "Latitud")]
        public double Latitud { get; set; }
        [Display(Name = "Longitud")]
        public double Longitud { get; set; }
        [Display(Name = "Altitud")]
        public double Altitud { get; set; }
        [Display(Name = "Tipo de Edificio")]
        public long TipoEdificioId { get; set; }
        [Display(Name = "Comuna")]
        public long? ComunaId { get; set; }
    }
}
