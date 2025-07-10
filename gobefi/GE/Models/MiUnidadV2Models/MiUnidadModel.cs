using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Models.MiUnidadV2Models
{
    public class MiUnidadModel
    {
        public List<SelectListItem> Instituciones { get; set; }
        public List<SelectListItem> Servicios { get; set; }
        public List<SelectListItem> Regiones { get; set; }
    }
}
