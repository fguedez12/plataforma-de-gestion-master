using GobEfi.Web.Core.Metadata;
using GobEfi.Web.Models.ComunaModels;
using GobEfi.Web.Models.TipoEdificioModels;
using GobEfi.Web.Models.TipoUsoModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EdificioModels
{
    [ModelMetadataType(typeof(EdificioMetadata))]
    public class EdificioModel : BaseModel<long>
    {

        public string Direccion { get; set; }
        [Required( ErrorMessage = "Número es obligatorio")]
        public string Numero { get; set; }
        [Required(ErrorMessage = "Calle es obligatorio")]
        public string Calle { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double Altitud { get; set; }
        public long? TipoEdificioId { get; set; }

        [DisplayName("Región")]
        [Required( ErrorMessage = "Región es obligatorio")]
        public long? RegionId { get; set; }

        [DisplayName("Provincia")]
        [Required(ErrorMessage = "Provincia es obligatorio")]
        public long? ProvinciaId { get; set; }
        [DisplayName("Comuna")]
        [Required(ErrorMessage = "Comuna es obligatorio")]
        public long? ComunaId { get; set; }

        public virtual ComunaModel Comuna { get; set; }
        public virtual TipoEdificioModel TipoEdificio { get; set; }

        public IEnumerable<TipoUsoModel> TiposDeUso { get; set; }
    }
}
