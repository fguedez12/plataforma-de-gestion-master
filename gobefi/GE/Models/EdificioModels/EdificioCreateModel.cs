using GobEfi.Web.Models.ComunaModels;
using GobEfi.Web.Models.TipoEdificioModels;
using GobEfi.Web.Models.TipoUsoModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.EdificioModels
{
    public class EdificioCreateModel : BaseModel<long>
    {
        public string Direccion { get; set; }
        public string Numero { get; set; }
        public string Calle { get; set; }
        public double Latitud { get; set; }
        public double Longitud { get; set; }
        public double Altitud { get; set; }
        public long? TipoEdificioId { get; set; }

        [DisplayName("Región")]
        public long RegionId { get; set; }
        [DisplayName("Provincia")]
        public long ProvinciaId { get; set; }
        [DisplayName("Comuna")]
        public long? ComunaId { get; set; }

        public virtual ComunaModel Comuna { get; set; }
        public virtual TipoEdificioModel TipoEdificio { get; set; }

        public IEnumerable<TipoUsoModel> TiposDeUso { get; set; }
    }
}
