using GobEfi.Web.Models.PermisosModels;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using GobEfi.Web.Core;
using GobEfi.Web.DTOs.AjustesDTO;
using Microsoft.EntityFrameworkCore;

namespace GobEfi.Web.Controllers
{
    //[Authorize(Roles = "USUARIO")]
    public class BaseController : Controller
    {
        protected readonly ApplicationDbContext context;
        protected readonly UserManager<Usuario> manager;
        protected readonly IHttpContextAccessor httpContextAccessor;
        protected readonly Usuario usuario;
        protected readonly PermisosModel permisos;
        protected readonly AjustesDTO ajustes;
        protected readonly PermisosAccion permisoAccion;
        protected readonly bool _isAdmin;
        protected readonly bool _isGestServ;
        protected readonly bool _userServiceIsPmg;
        protected readonly IUsuarioService userService;

        public BaseController(
            ApplicationDbContext context,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService service)
        {
            this.context = context;
            this.manager = manager;
            this.httpContextAccessor = httpContextAccessor;
            this.usuario = (manager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
            this.userService = service;
            this.permisos = GetPermisions();
            this.ajustes =  GetAjustes();
            this._userServiceIsPmg = UserServiceIsPMG();

            this.permisoAccion = GetPermisosAccion();
            if (usuario != null) {
                _isAdmin = manager.IsInRoleAsync(usuario, Constants.Claims.ES_ADMINISTRADOR).Result;
                _isGestServ = manager.IsInRoleAsync(usuario, Constants.Claims.ES_GESTORSERVICIO).Result;
            }

           
        }

        /// <summary>
        /// Retorna un listado de 1900 hasta el año actual para los drop down list
        /// </summary>
        /// <returns></returns>
        public List<SelectListItem> Anios()
        {
            List<SelectListItem> lista = new List<SelectListItem>();
            for (int i = 1900; i < DateTime.Now.Year; i++)
            {
                var item = new SelectListItem(i.ToString(), i.ToString());

                lista.Add(item);
            }

            return lista;
        }

        public BaseController(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor, IUsuarioService usuarioService)
        {
            this.context = context;
            this.httpContextAccessor = httpContextAccessor;
        }

        protected PermisosAccion GetPermisosAccion()
        {
            return userService.GetPermisosAccion(this.usuario, httpContextAccessor.HttpContext.Request.Path.Value);
        }

        protected string NombreCompletoUsuario(string idUsuario)
        {
            return userService.NombreCompletoUsuario(idUsuario);
        }
        
        protected PermisosModel GetPermisions()
        {
            var permisos = userService.TienePermisos(this.usuario, httpContextAccessor.HttpContext.Request.Path.Value);
            return permisos;
        }

        protected AjustesDTO GetAjustes()
        {
            var ajusteFDB = context.Ajustes.FirstOrDefault();
            var ajusteDTO = new AjustesDTO();
            ajusteDTO.EditUnidadPMG = ajusteFDB.EditUnidadPMG;
            ajusteDTO.DeleteUnidadPMG = ajusteFDB.DeleteUnidadPMG;
            ajusteDTO.ComprasServicio = ajusteFDB.ComprasServicio;
            ajusteDTO.CreateUnidadPMG = ajusteFDB.CreateUnidadPMG;
            return ajusteDTO;

        }

        protected bool UserServiceIsPMG()
        {
            var servicioUsuario = context.UsuariosServicios
                .Where(x => x.UsuarioId == usuario.Id)
                .Include(x=>x.Servicio).Select(x => x.Servicio)
                .FirstOrDefault();

            if (servicioUsuario != null)
            {
                return servicioUsuario.ReportaPMG;
            }

            return false;
        }

        protected PermisosModel GetPermisions(int servicioId)
        {
            return userService.TienePermisos(this.usuario, httpContextAccessor.HttpContext.Request.Path.Value);
        }

        protected PermisosModel GetPermisions(string path)
        {
            return userService.TienePermisos(this.usuario, path);
        }

        protected SortModel SortItems()
        {
            var order = new SortModel();
            return order;
        }
    }
}
