﻿using api_gestiona.DTOs.Pagination;
using api_gestiona.DTOs.Viajes;
using api_gestiona.Entities;
using api_gestiona.Helpers;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ViajesController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ViajesController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{servicioId}/{year}")]
        public async Task<ActionResult> Get([FromRoute] long servicioId, [FromQuery] PaginationDTO paginationDTO,[FromRoute] int year)
        {
            var queryable = _context.Viajes.Where(x => x.ServicioId == servicioId && x.Active && x.Year == year)
                .Include(x=>x.AeropuertoOrigen)
                .Include(x=>x.AeropuertoDestino)
                .AsQueryable();
            var noDeclara = await _context.Servicios.Where(x => x.Id == servicioId).Select(x => year == 2025 ? x.NoDeclaraViajeAvion2025 : x.NoDeclaraViajeAvion).FirstOrDefaultAsync();
            await HttpContext.InsertPaginationParameterHeaders(queryable);
            var entities = await queryable.Pagin(paginationDTO).ToListAsync();
            var dtos = _mapper.Map<List<ViajeListDTO>>(entities);
            var response = new ViajeResponseDTO { Viajes = dtos, NoDeclaraViajeAvion = noDeclara };
            return Ok(response);

        }

        [HttpPut("no-declara/{id}/{year}")]
        public async Task<ActionResult> PutNoDeclara([FromRoute] int id, [FromQuery] bool value,[FromRoute] int year)
        {
            var entity = await _context.Servicios.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) { return BadRequest("El recurso solicitado no existe"); }
            if (year == 2025)
                entity.NoDeclaraViajeAvion2025 = value;
            else
                entity.NoDeclaraViajeAvion = value;
            _context.Servicios.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpGet("get-by-id/{Id}")]
        public async Task<ActionResult> GetById([FromRoute] long Id)
        {
            var entity = await _context.Viajes.Where(x => x.Id == Id && x.Active)
                .Include(x=>x.AeropuertoOrigen)
                .ThenInclude(a=>a.Pais)
                .Include(x => x.AeropuertoDestino)
                .ThenInclude(a => a.Pais)
                .FirstOrDefaultAsync();
            var dto = _mapper.Map<ViajeEditDTO>(entity);
            return Ok(dto);

        }

        [HttpPost("{servicioId}/{year}")]
        public async Task<ActionResult> Post([FromRoute] long servicioId, ViajeCreateDTO model,[FromRoute] int year)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var entity = _mapper.Map<Viaje>(model);
            entity.ServicioId = servicioId;
            entity.CreatedAt = DateTime.Now;
            entity.CreatedBy = userId;
            entity.Active = true;
            entity.Version = 1;
            entity.Year = year;
            _context.Viajes.Add(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{viajeId}/{year}")]
        public async Task<ActionResult> Put([FromRoute] long viajeId, ViajeCreateDTO model,[FromRoute] int year)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var entity = await _context.Viajes.FirstOrDefaultAsync(x => x.Id == viajeId);
            if (entity == null) { return BadRequest("El recurso solicitado no existe"); }
            entity.UpdatedAt = DateTime.Now;
            entity.ModifiedBy = userId;
            entity.Version = entity.Version + 1;
            entity.Periodo = model.Periodo;
            entity.CiudadOrigen = model.CiudadOrigen;
            entity.CiudadDestino = model.CiudadDestino;
            entity.AeropuertoOrigenId = model.AeropuertoOrigenId;
            entity.AeropuertoDestinoId = model.AeropuertoDestinoId;
            entity.NViajesSoloIdaRetorno = model.NViajesSoloIdaRetorno;
            entity.NViajesIdaVuelta = model.NViajesIdaVuelta;
            entity.Distancia = model.Distancia;
            entity.Year = year;
            _context.Viajes.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{viajeId}")]
        public async Task<ActionResult> Delete([FromRoute] long viajeId)
        {
            var userId = User.Claims.First(i => i.Type == "userId").Value;
            var entity = await _context.Viajes.FirstOrDefaultAsync(x => x.Id == viajeId);
            if (entity == null) { return BadRequest("El recurso solicitado no existe"); }
            entity.UpdatedAt = DateTime.Now;
            entity.ModifiedBy = userId;
            entity.Version = entity.Version + 1;
            entity.Active = false;
            _context.Viajes.Update(entity);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
