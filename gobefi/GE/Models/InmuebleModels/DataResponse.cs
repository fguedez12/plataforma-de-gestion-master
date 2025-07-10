using GobEfi.Web.Models.InstitucionModels;
using GobEfi.Web.Models.ServicioModels;
using GobEfi.Web.Models.UnidadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.InmuebleModels
{
    public class DataResponse
    {
        public bool Ok { get; set; }
        public string Message { get; set; }
        public IEnumerable<InstitucionListModel> Instituciones { get; set; }
        public IEnumerable<ServicioListModel> Servicios { get; set; }
        public List<UnidadModel> Unidades { get; set; }
    }
}
