using GobEfi.Web.Core.Metadata;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EnergeticoModels
{
    [ModelMetadataType(typeof(EnergeticoMetadata))]
    public class EnergeticosIndexModel : BaseIndexModel<EnergeticoModel>
    {
        public string Nombre { get; set; }
        public bool Activo { get; set; }
        public bool PermiteMedidor { get; set; }
    }
}
