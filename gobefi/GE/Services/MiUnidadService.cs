using AutoMapper;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.InstitucionModels;
using GobEfi.Web.Models.MenuModels;
using GobEfi.Web.Models.MiUnidadV2Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class MiUnidadService : IMiUnidadService
    {
        private readonly IInstitucionService _institucionService;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly IMenuService _menuService;
        private readonly IUnidadService _unidadService;
        private readonly Usuario _currentUser;
        private readonly bool _isAdmin;
        private readonly string _nombreModuloMI = "Consumos Inteligentes";

        public MiUnidadService(
            IInstitucionService institucionService,
            ApplicationDbContext context, IMapper mapper, UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor,
            IMenuService menuService,
            IUnidadService unidadService
            )
        {
            _institucionService = institucionService;
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _menuService = menuService;
            _unidadService = unidadService;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
            _isAdmin = _userManager.IsInRoleAsync(_currentUser, Constants.Claims.ES_ADMINISTRADOR).Result;
        }
        public MiUnidadModel GetNewModel()
        {
            return new MiUnidadModel();
        }

        public async Task<List<SelectListItem>> SetInstituciones()
        {
            var lista = await _institucionService.GetAsociadosByUserId(_currentUser.Id);
            return MapToSelectListItem(lista);
        }

        private List<SelectListItem> MapToSelectListItem(IEnumerable<InstitucionListModel> list)
        {
            var selectList = new List<SelectListItem>();
            foreach (var item in list)
            {
                selectList.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Nombre });
            }

            return selectList;
        }

        public async Task<ICollection<MenuModel>> GetSubMenusByMenuAndRolesIncludeMi(string rutaActual, string usuarioId, long divisionId)
        {
            var exist = await _unidadService.HasInteligentMeasurement(divisionId);
            var menuList = await _menuService.GetSubMenusByMenuAndRoles(rutaActual, usuarioId);
            var result = new List<MenuModel>();
            if (!exist)
            {
                foreach (var menu in menuList)
                {
                    if (menu.Nombre == _nombreModuloMI)
                    {
                        continue;
                    }

                    result.Add(menu);
                }

                return result;
            }
            return menuList;
        }
    }
}
