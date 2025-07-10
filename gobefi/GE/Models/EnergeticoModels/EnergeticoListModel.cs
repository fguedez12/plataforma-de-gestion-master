using GobEfi.Web.Core.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace GobEfi.Web.Models.EnergeticoModels
{

    [ModelMetadataType(typeof(EnergeticoMetadata))]
    public class EnergeticoListModel : BaseModel<long>
    {
        public string Icono { get; set; }
        public string Nombre { get; set; }
        public bool Activo { get; set; }
    }
}
