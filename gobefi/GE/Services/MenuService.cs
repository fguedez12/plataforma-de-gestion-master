using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.MenuModels;
using GobEfi.Web.Models.MenuPanelModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using GobEfi.Web.Data;
using Microsoft.AspNetCore.Identity;
using GobEfi.Web.Data.Entities;

namespace GobEfi.Web.Services
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository _repoMenu;
        private readonly IPermisosRepository _repoPermisos;
        private readonly IUsuarioRepository _repoUsuarios;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly ILogger _logger;
        private readonly string DOCUMENTOS_ETAPA1 = "Documentos Etapa 1";
        private readonly string DOCUMENTOS_ETAPA2 = "Documentos Etapa 2";

        public MenuService(
            IMenuRepository repoMenu,
            IPermisosRepository repoPermisos,
            IUsuarioRepository repoUsuarios,
            IMapper mapper,
            ApplicationDbContext context,
            UserManager<Usuario> userManager,
            ILoggerFactory factory)
        {
            _repoPermisos = repoPermisos;
            _repoUsuarios = repoUsuarios;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
            _repoMenu = repoMenu;
            _logger = factory.CreateLogger<MenuService>();
        }

        public IEnumerable<MenuModel> All()
        {
            throw new System.NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public MenuModel Get(long id)
        {
            throw new System.NotImplementedException();
        }

        public ICollection<MenuPanelModel> GetByUser(string userId)
        {
            var usuarioRoles = _repoUsuarios.Query().Include(ur => ur.UsuarioRoles).FirstOrDefault(u => u.Id == userId).UsuarioRoles;
            
            var ret = _repoPermisos.Query().Where(p => 
            usuarioRoles.Any(a => a.RoleId == p.RolId)
            && p.Lectura)
            .Include(m => m.Menu.MenuPanel)
            .OrderBy(o => o.Menu.Orden)
            .ToList();

            var menu = ret.Select(m => m.Menu.MenuPanel).Distinct().Where(mp => mp != null).ToList();

            List<MenuPanelModel> listaMenu = _mapper.Map<List<MenuPanelModel>>(menu);

            return listaMenu;
        }

        public async Task<ICollection<MenuModel>> GetSubMenusByMenuAndRoles(string rutaActual, string usuarioId, long? servicioIdEv = null)
        {

            var user = await _userManager.FindByIdAsync(usuarioId);

            var isAdmin = await _userManager.IsInRoleAsync(user, "ADMINISTRADOR");

            var servicio = await _context.UsuariosServicios
                .Include(x => x.Servicio)
                .Where(x => x.UsuarioId == usuarioId)
                .Select(x => x.Servicio).FirstOrDefaultAsync();

            string[] urlSplit = rutaActual.Split('/');

            bool esNumero = long.TryParse(urlSplit[urlSplit.Length - 1], out long numero);

            if (esNumero)
            {
                rutaActual = rutaActual.Replace(urlSplit[urlSplit.Length - 1], "");
            }
            
            if (!_repoMenu.Query().Any(r => r.Url == rutaActual))
            {
                return null;
            }

            var pagina = await _repoMenu.Query()
                .Where(u => u.Url == rutaActual)
                .FirstOrDefaultAsync();
            long paginaId = pagina.Id;

            var usuario = await _repoUsuarios.Query()
                .Include(ur => ur.UsuarioRoles)
                .Where(u => u.Id == usuarioId)
                .FirstOrDefaultAsync();
            var usuarioRoles = usuario.UsuarioRoles;

            var permisos = await _repoPermisos.Query()
                .Where(p => p.MenuId == paginaId
                    && usuarioRoles.Any(a => a.RoleId == p.RolId)
                    && p.Lectura)
                .ToListAsync();

            List<MenuModel> listaSubMenu = new List<MenuModel>();
            foreach (var item in permisos)
            {
                var subMenus = await _repoMenu.Query()
                    .Where(s => s.MenuId == item.MenuId)
                    .OrderBy(s=>s.Orden)
                    .ToListAsync();

                foreach (var subMenu in subMenus)
                {
                    // si no tiene activo el campo lectura en la tabla permisos no se muestra por pantalla
                    if (!_repoPermisos.Query().Any(tp => tp.MenuId == subMenu.Id && tp.Lectura && usuarioRoles.Any(r => r.RoleId == tp.RolId))) 
                        continue;


                    //TODO: Validar si es Documentos Etapa 1 o 2

                    if (subMenu.Nombre == DOCUMENTOS_ETAPA2 && !isAdmin)
                    {
                        if (servicioIdEv != null && servicioIdEv>0) 
                        {
                            var serviciEv = await _context.Servicios.FirstOrDefaultAsync(x=>x.Id == servicioIdEv);
                            if (serviciEv.EtapaSEV > 1)
                            {
                                var paraLista = _mapper.Map<MenuModel>(subMenu);

                                if (listaSubMenu.Exists(a => a.Id == paraLista.Id))
                                    continue;

                                listaSubMenu.Add(paraLista);
                            }

                        }
                    }
                    else 
                    {
                        var paraLista = _mapper.Map<MenuModel>(subMenu);

                        if (listaSubMenu.Exists(a => a.Id == paraLista.Id))
                            continue;

                        listaSubMenu.Add(paraLista);
                    }
                }
            }
            
            return listaSubMenu;
        }

        public long Insert(MenuModel model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(MenuModel model)
        {
            throw new System.NotImplementedException();
        }
    }
}