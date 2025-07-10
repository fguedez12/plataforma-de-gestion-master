using GobEfi.Web.Core.Metadata;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace GobEfi.Web.Models.EnergeticoModels
{
    [ModelMetadataType(typeof(EnergeticoMetadata))]
    public class EnergeticoModel : BaseModel<long>
    {
        public string Icono { get; set; }
        public string Nombre { get; set; }
        public int Posicion { get; set; }
        [DisplayName("Permite medidor")]
        public bool PermiteMedidor { get; set; }
        public bool Activo { get; set; }
        public bool PermiteTipoTarifa { get; set; }
        public bool PermitePotenciaSuministrada { get; set; }
        public string ClassIconActivo { get { return this.Activo ? "active" : string.Empty; } }
        public string ClassTextActivo { get { return this.Activo ? "text-primary" : "text-muted"; } }
        public string ClassLinkDisabled { get { return this.Activo ? string.Empty : "btn disabled text-muted"; } }
        public string ClassLinkHidden { get { return this.PermiteMedidor ? string.Empty : "invisible"; } }
    }
}
