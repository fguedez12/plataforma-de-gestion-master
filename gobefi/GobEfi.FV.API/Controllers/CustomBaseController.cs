using AutoMapper;
using GobEfi.FV.API.Models.DTOs;
using GobEfi.FV.API.Models.Entities;
using GobEfi.FV.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomBaseController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected async Task<List<TDTO>> Get<TEntidad, TDTO>() where TEntidad : class
        {
            var entidades = await _context.Set<TEntidad>().AsNoTracking().ToListAsync();
            var dtos = _mapper.Map<List<TDTO>>(entidades);
            return dtos;
        }

        protected async Task<ActionResult<TDTO>> Get<TEntidad, TDTO>(int id) where TEntidad : class, IId
        {
            var entidad = await _context.Set<TEntidad>().AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            if (entidad == null)
            {
                return NotFound();
            }

            return _mapper.Map<TDTO>(entidad);
        }

        protected async Task<ActionResult> Post<TCreacion, TEntidad, TLectura>
            (TCreacion creacionDTO, string nombreRuta) where TEntidad : class, IId
        {
            var entidad = _mapper.Map<TEntidad>(creacionDTO);
            _context.Add(entidad);
            await _context.SaveChangesAsync();
            var dtoLectura = _mapper.Map<TLectura>(entidad);
            return new CreatedAtRouteResult(nombreRuta, new { id = entidad.Id }, dtoLectura);
        }

        protected TEntidad SetAuditCreate<TEntidad>(TEntidad entidad) where TEntidad : class, IId,IAuditable
        {
            entidad.Active = true;
            entidad.CreatedAt = DateTime.Now;
            entidad.CreatedBy = this.User.FindFirst(i => i.Type == "Id").Value;
            entidad.Version = 1;
            return entidad;
            
        }

        protected TEntidad SetAuditUpdate<TEntidad>(TEntidad entidad) where TEntidad : class, IId, IAuditable
        {
            entidad.ModifiedAt = DateTime.Now;
            entidad.ModifiedBy = this.User.FindFirst(i => i.Type == "Id").Value;
            entidad.Version = entidad.Version+1;
            return entidad;
        }

        protected CurrentUserDTO GetCurrenUser()
        {
            if (this.User.Claims.Count() > 0)
            {
                var user = new CurrentUserDTO()
                {
                    Id = User.FindFirst( i=>i.Type == "Id").Value,
                    Email = User.FindFirst(ClaimTypes.Email).Value,
                    Nombre = User.FindFirst(i => i.Type == "Nombre").Value,
                    Role = User.FindFirst(ClaimTypes.Role).Value,
                    Sexo = User.FindFirst(i => i.Type =="Sexo").Value,
                    ServicioId = Convert.ToInt64(User.FindFirst("ServicioId").Value)
                };
                return user;
            }

            return new CurrentUserDTO();
        }
    }
}
