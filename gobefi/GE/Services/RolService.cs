using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.InstitucionModels;
using GobEfi.Web.Models.RolModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;

namespace GobEfi.Web.Services
{
    public class RolService : IRolService
    {
        private IRolRepository _repoRol;
        private readonly RoleManager<Rol> _rolManager;
        private ILogger _logger;
        private IMapper _mapper;
        private readonly UserManager<Usuario> _userManager;
        private readonly Usuario _currentUser;

        public RolService(
            IRolRepository repository,
            ILoggerFactory factory,
            RoleManager<Rol> rolManager,
            IMapper mapper, 
            UserManager<Usuario> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _repoRol = repository;
            _rolManager = rolManager;
            _logger = factory.CreateLogger<RolService>();
            _mapper = mapper;
            _userManager = userManager;

            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;

        }

        public IEnumerable<RolModel> All()
        {
            return _repoRol
                .All()
                .OrderBy(o => o.Nombre)
                .ProjectTo<RolModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public async Task<IEnumerable<RolListModel>> AllByUser(string id)
        {
            var result = await _repoRol.AllByUser(id);
            return result;
        }

        public void Delete(string id)
        {
            var toDelete = _repoRol.Get(id);

            _repoRol.Delete(toDelete);
            _repoRol.SaveChanges();
        }

        public RolModel Get(string id)
        {
            var rol = _repoRol.Get(id);
            if (rol == null)
            {
                throw new NotFoundException(nameof(rol));
            }

            return _mapper.Map<RolModel>(rol);
        }

        public string Insert(RolModel model)
        {
            var rolEntity = _mapper.Map<Rol>(model);

            _repoRol.Insert(rolEntity);

            _repoRol.SaveChanges();

            return rolEntity.Id;
        }

        public void Update(RolModel model)
        {
            var role = _rolManager.FindByIdAsync(model.Id).Result;
            role.Nombre = model.Nombre;
            role.Name = model.Nombre.ToUpper();
            

      
            var result = _rolManager.UpdateAsync(role).Result;
   

               
            // _repoRol.UpdateRol(entity);
            //_repoRol.Update(entity);
            //_repoRol.SaveChanges();

        }

        public IEnumerable<RolModel> GetByUserId(string id)
        {
            return _repoRol
                .Query()
                .Include(ur => ur.UsuarioRoles)
                .Where(rol => rol.UsuarioRoles.Any(ur => ur.UserId == id))
                .OrderBy(o => o.Nombre)
                .ProjectTo<RolModel>(_mapper.ConfigurationProvider)
                .ToList();
        }

        public IEnumerable<RolModel> GetNoAsociadasByUserId(string id)
        {
            var user = _userManager.FindByIdAsync(id).Result;

            IList<string> parameterUserRoles = _userManager.GetRolesAsync(user).Result;

            IList<string> currentUserRoles = _userManager.GetRolesAsync(_currentUser).Result;

            var result = GetRolesAnidados(currentUserRoles).Result;



            var roleActual =  result.Where(r => parameterUserRoles.Contains(r.NormalizedName));

            result = result.Where(r => !roleActual.Any(re => re.NormalizedName == r.NormalizedName));



            return result;

            //var query = _repoRol.Query();
            //query = query.Include(ur => ur.UsuarioRoles).Where(rol => !rol.UsuarioRoles.Any(ur => ur.UserId == id));


            //return query.OrderBy(o => o.Nombre).ProjectTo<RolModel>(_mapper.ConfigurationProvider).ToList();
        }

        public async Task<IEnumerable<RolModel>> GetByCurrentUserRol()
        {
            IList<string> roles = await _userManager.GetRolesAsync(_currentUser);

            var result = await GetRoles(roles);

            return result;
        }

        private async Task<IEnumerable<RolModel>> GetRoles(IList<string> roleParentsNames)
        {
            var anidados = await GetRolesAnidados(roleParentsNames);

            List<RolModel> finalList = new List<RolModel>();
            foreach (var item in roleParentsNames)
            {
                var roleParentObject = await _rolManager.FindByNameAsync(item);

                finalList.Add(_mapper.Map<RolModel>(roleParentObject));
            }

            foreach (var item in anidados)
            {
                finalList.Add(item);
            }


            return finalList;
        }

        private async Task<IEnumerable<RolModel>> GetRolesAnidados(IList<string> roleParentsNames)
        {
            List<RolModel> roleList = new List<RolModel>();
            var query = _repoRol.Query();

            foreach (string roleName in roleParentsNames)
            {
                var roleParentObject = await _rolManager.FindByNameAsync(roleName);


                var rolesObject = query.Where(r => r.DependeDelRoleId == roleParentObject.Id);
                var listRoles = rolesObject.ProjectTo<RolModel>(_mapper.ConfigurationProvider).ToList();

                foreach (var item in listRoles)
                {
                    roleList.Add(item);

                    var childsRole = await GetRolesAnidados(new List<string> { item.NormalizedName });
                    foreach (var childRole in childsRole)
                    {
                        roleList.Add(childRole);
                    }
                }
            }

            return roleList;
        }
    }
}
