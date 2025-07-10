using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EdificioModels
{
    public class EdificioIndexModel : BasePagedModel<EdificioListModel>
    {
        public long Id { get; set; }

        [DisplayName("Dirección")]
        public string Direccion { get; set; }

        [DisplayName("Regiones")]
        public long RegionId { get; set; }

        [DisplayName("Comunas")]
        public long ComunaId { get; set; }

    }
}
