using GobEfi.Web.Core.Metadata;
using Microsoft.AspNetCore.Mvc;

namespace GobEfi.Web.Models.EnergeticoDivisionModels
{
    [ModelMetadataType(typeof(EnergeticoDivisionMetadata))]
    public class EnergeticoDivisionModel : BaseModel<long>
    {
        public long DivisionId { get; set; }
        public long EnergeticoId { get; set; }
        public long? NumeroClienteId { get; set; }
        public long UnidadMedidaId { get; set; }
    }
}
