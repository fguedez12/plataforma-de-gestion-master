using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.AreaModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class AreaService : IAreaService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<Usuario> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        private readonly Usuario _currentUser;

        public AreaService(ApplicationDbContext context, UserManager<Usuario> userManager, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
        }

        public async Task<List<AreaModel>> Save(AreaForSave model) 
        {
            var newArea = new Area() {
                Active = true,
                CreatedAt = DateTime.Now,
                CreatedBy = _currentUser.Id,
                Nombre = model.Nombre,
                PisoId = model.PisoId,
                Superficie = model.Superficie,
                TipoUsoId = model.TipoUsoId,
                Version = 1,
                NroRol = model.NroRol

            };

            _context.Areas.Add(newArea);
            await _context.SaveChangesAsync();

            var pisoFromDb = await _context.Pisos.Include(p => p.Areas).Where(p => p.Id == model.PisoId).FirstOrDefaultAsync();

            foreach (var area in pisoFromDb.Areas.ToList())
            {
                if (!area.Active) {
                    pisoFromDb.Areas.Remove(area);
                }
            }

            return _mapper.Map<List<AreaModel>>(pisoFromDb.Areas.OrderBy(a=>a.Id));
        }

        public async Task AddUnidad(AreaUnidadRequest model)
        {
            var up = new UnidadArea()
            {
                AreaId = model.AreaId,
                UnidadId = model.UnidadId
            };

            _context.UnidadesAreas.Add(up);
            await _context.SaveChangesAsync();

        }

        public async Task RemUnidad(AreaUnidadRequest model)
        {
            var up = await _context.UnidadesAreas
                .Where(upi => upi.AreaId == model.AreaId && upi.UnidadId == model.UnidadId)
                .FirstOrDefaultAsync();

            _context.UnidadesAreas.Remove(up);
            await _context.SaveChangesAsync();

        }

    }
}
