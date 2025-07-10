using GobEfi.Web.Core.Metadata;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DivisionModels
{
    [ModelMetadataType(typeof(DivisionMetadata))]
    public class DivisionListModel : BaseModel<long>
    {
        public string Nombre { get; set; }
        public bool ReportaPMG { get; set; }
        public int AnyoConstruccion { get; set; }
        public bool Selected { get; set; }
        public string Pisos { get; set; }
        public long ServicioId { get; set; }
        public long InstitucionId { get; set; }

        public bool ServicioEsPMG { get; set; }
        public string Direccion { get; set; }
    }
}
