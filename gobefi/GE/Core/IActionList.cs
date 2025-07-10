using GobEfi.Web.Models.PermisosModels;
using GobEfi.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.DTOs.AjustesDTO;

namespace GobEfi.Web.Core
{
    public class IActionList
    {
        public string Id { get; set; }
        public PermisosModel Permisos { get; set; }
        public PermisoAcciones Acciones { get; set; }
        public bool PMG { get; set; }
        public AjustesDTO Ajustes { get; set; }
        public bool IsAdmin { get; set; }
    }
}
