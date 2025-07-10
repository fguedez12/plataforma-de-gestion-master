using GobEfi.Web.Core.Metadata;
using GobEfi.Web.Models.ComunaModels;
using GobEfi.Web.Models.TipoEdificioModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EdificioModels
{
    [ModelMetadataType(typeof(EdificioMetadata))]
    public class EdificioListModel : BaseModel<long>
    {
        public string Direccion { get; set; }
        public long TipoEdificioId { get; set; }
        public long? ComunaId { get; set; }

        public virtual ComunaModel Comuna { get; set; }
        public virtual TipoEdificioModel TipoEdificio { get; set; }
    }
}
