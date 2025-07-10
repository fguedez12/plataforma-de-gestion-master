using GobEfi.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.ComunaModels
{
    public class ComunaModel : BaseModel<long>
    {
        public long? ProvinciaId { get; set; }
        public long? RegionId { get; set; }
        public virtual Provincia Provincia { get; set; }
        public virtual Region Region { get; set; }
        public string Nombre { get; set; }

    }
}
