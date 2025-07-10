using GobEfi.Web.Core.Metadata;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.DivisionModels
{
    [ModelMetadataType(typeof(DivisionMetadata))]
    public class DivisionIndexModel : BasePagedModel<DivisionListModel>
    {
        public long Id { get; set; }
        public string Nombre { get; set; }
        public bool ReportaPMG { get; set; }
        public int AnyoConstruccion { get; set; }
        public string Pisos { get; set; }

        [DisplayName("Activo")]
        public bool Active { get; set; }

        [DisplayName("Instituciones")]
        public long InstitucionId { get; set; }
        [DisplayName("Servicios")]
        public long ServicioId { get; set; }
        public bool Activo { get; set; } = true;

        [DisplayName("Dirección")]
        public string Direccion { get; set; }
    }
}
