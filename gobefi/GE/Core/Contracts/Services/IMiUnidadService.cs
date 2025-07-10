using GobEfi.Web.Models.MenuModels;
using GobEfi.Web.Models.MiUnidadV2Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IMiUnidadService
    {
        MiUnidadModel GetNewModel();
        Task<ICollection<MenuModel>> GetSubMenusByMenuAndRolesIncludeMi(string rutaActual, string usuarioId, long divisionId);
        Task<List<SelectListItem>> SetInstituciones();
    }
}
