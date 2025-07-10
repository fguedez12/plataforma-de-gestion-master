using GobEfi.Web.Models.PermisosModels;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.RolModels;
using GobEfi.Web.Models.UsuarioModels;
using GobEfi.Web.Services.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Contracts.Services
{
    public interface IUsuarioService : IService<UsuarioModel, string>
    {
        PagedList<UsuarioListModel> Paged(UsuarioRequest request);
        Task<UsuarioModel> UpdateRoles(ICollection<RolModel> rols);
        PermisosModel TienePermisos(Usuario usuario, string rutaActual);
        string NombreCompletoUsuario(string id);
        IEnumerable<UsuarioListExcelModel> Exportar();
        Task<int> AgregarAsociaciones(UsuarioModel model);
        bool ExisteEmail(string email);
        PermisosAccion GetPermisosAccion(Usuario usuario, string value);

        void UpdateSexo(string usuarioId, int sexoId);
    }
}
