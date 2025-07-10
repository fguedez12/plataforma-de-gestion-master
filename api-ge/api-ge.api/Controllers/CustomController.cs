using api_gestiona.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;

namespace api_gestiona.Controllers.V1
{
    public class CustomController : ControllerBase
    {

        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _manager;
        private readonly IConfiguration _configuration;
        protected Usuario _usuario;
        protected bool _isAdmin;

        public CustomController(IMapper mapper, ApplicationDbContext context, UserManager<Usuario> manager, IConfiguration configuration = null)
        {

            _mapper = mapper;
            _context = context;
            _manager = manager;
            _configuration = configuration;
        }


        protected bool IsAppVaidate(HttpRequest request)
        {
            string HeaderKeyName = _configuration["application-key:header"];
            string HeaderKeyValue = _configuration["application-key:value"];
            request.Headers.TryGetValue(HeaderKeyName, out StringValues headerValue);
            string headerValueString = headerValue.FirstOrDefault();
            if (string.IsNullOrEmpty(headerValue))
            {
                return false;
            }

            if (HeaderKeyValue != headerValueString)
            {
                return false;
            }

            return true;
        }

        protected async Task setUser(string userId)
        {
            _usuario = await _manager.FindByIdAsync(userId);
            if (_usuario != null)
            {
                _isAdmin = await _manager.IsInRoleAsync(_usuario, "ADMINISTRADOR");
            }
        }

        protected async Task<bool> userInService(string userId, long servicioId)
        {
            _usuario = await _manager.FindByIdAsync(userId);
            if (_usuario != null)
            {
                _isAdmin = await _manager.IsInRoleAsync(_usuario, "ADMINISTRADOR");
            }

            if (_isAdmin)
            {
                return true;
            }

            var exist = _context.UsuariosServicios.Any(x => x.UsuarioId == userId && x.ServicioId == servicioId);
            if (exist)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        protected async Task<List<TDTO>> Get<TEntity, TDTO>() where TEntity : class
        {
            try
            {
                var entities = await _context.Set<TEntity>().AsNoTracking().ToListAsync();
                return _mapper.Map<List<TDTO>>(entities);
            }
            catch (Exception ex)
            {

                throw ex;
            }


        }

        protected TEntity SetAuditableSave<TEntity>(TEntity entity, string userId) where TEntity : BaseEntity
        {
            entity.Active = true;
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = userId;
            entity.Version = 1;
            return entity;
        }

        protected TEntity SetAuditableUpdate<TEntity>(TEntity entity, string userId, TEntity entityDb) where TEntity : BaseEntity
        {
            entity.CreatedAt = entityDb.CreatedAt;
            entity.CreatedBy = entityDb.CreatedBy;
            entity.Active = true;
            entity.UpdatedAt = DateTime.Now;
            entity.ModifiedBy = userId;
            entity.Version = entityDb.Version + 1;
            return entity;
        }
        protected TEntity SetAuditableDelete<TEntity>(TEntity entity, string userId) where TEntity : BaseEntity
        {
            entity.Active = false;
            entity.UpdatedAt = DateTime.Now;
            entity.ModifiedBy = userId;
            entity.Version = entity.Version + 1;
            return entity;
        }
    }
}
