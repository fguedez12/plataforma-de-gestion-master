using GobEfi.Web.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.ProvinciaModels
{
    public class ProvinciaModel : BaseModel<long>
    {
        public long RegionId { get; set; }
        public string Nombre { get; set; }
        public virtual Region Region { get; set; }
        public ICollection<Comuna> Comunas { get; set; }
    }
}
