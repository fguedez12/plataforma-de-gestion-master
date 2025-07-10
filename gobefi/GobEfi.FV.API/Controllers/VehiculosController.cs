    using AutoMapper;
using GobEfi.FV.API.Filters;
using GobEfi.FV.API.Helpers;
using GobEfi.FV.API.Models.DTOs;
using GobEfi.FV.API.Models.Entities;
using GobEfi.FV.API.Services;
using GobEfi.FV.Shared.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VehiculosController : CustomBaseController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IFileStorage _fileStorage;
        private readonly string Container = "vehiculos";
        private readonly ILogger<VehiculosController> _logger;


        public VehiculosController(ApplicationDbContext context,IMapper mapper, IFileStorage  fileStorage, 
            ILogger<VehiculosController> logger) 
            : base(context,mapper)
        {
            _context = context;
            _mapper = mapper;
            _fileStorage = fileStorage;
            _logger = logger;
           
        }
        
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult<List<VehiculoDTO>>> Get([FromQuery] PaginationDTO paginationDTO)
        {
            var _currentUser = GetCurrenUser();
            var queryable = _context.Vehiculos.Where(x => x.Active).AsQueryable();

            if (_currentUser.Role == "ADMINISTRADOR")
            {
                await HttpContext.InsertPaginationParams(queryable, paginationDTO.PerPage);

                var listFromDb = await queryable.Pagin(paginationDTO)
                    .Include(x => x.Modelo)
                    .Include(x => x.Imagenes)
                    .OrderBy(x => x.Patente)
                    .ToListAsync();
                return _mapper.Map<List<VehiculoDTO>>(listFromDb);
            }
            else
            {
                queryable = queryable.Where(x => x.ServicioId == _currentUser.ServicioId);
                await HttpContext.InsertPaginationParams(queryable, paginationDTO.PerPage);

                var listFromDb = await queryable.Pagin(paginationDTO)
                    .Include(x => x.Modelo)
                    .Include(x => x.Imagenes)

                    .OrderBy(x => x.Patente)
                    .ToListAsync();
                return _mapper.Map<List<VehiculoDTO>>(listFromDb);
            }
        }
        [HttpGet("getbypatente")]
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult<List<VehiculoDTO>>> GetByPatente([FromQuery] string patente)
        {
            var _currentUser = GetCurrenUser();


            if (!string.IsNullOrEmpty(patente))
            {
                var listaFromDb = await _context.Vehiculos
               .Include(x => x.Modelo)
               .Include(x => x.Imagenes)
               .Where(x => x.Active)
               .OrderBy(x => x.Patente)
               .Where(x => x.Patente.Contains(patente)).ToListAsync();
                return _mapper.Map<List<VehiculoDTO>>(listaFromDb);
            }
            else 
            {
                var listaFromDb = await _context.Vehiculos
               .Include(x => x.Modelo)
               .Include(x => x.Imagenes)
               .Where(x => x.Active)
               .OrderBy(x => x.Patente)
               .ToListAsync();
                return _mapper.Map<List<VehiculoDTO>>(listaFromDb);
            }

        }

        [HttpGet("filtro")]
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult<List<VehiculoDTO>>> GetFiltro([FromQuery] FiltroVehiculosDTO filtro)
        {
            var _currentUser = GetCurrenUser();
            var vehiculosQueryable = _context.Vehiculos.Include(v=>v.Modelo).AsQueryable();
            if (filtro.ModeloId!=null)
            {
                vehiculosQueryable = vehiculosQueryable.Where(x => x.Modelo.Id == filtro.ModeloId);
            }
           
            
            if (filtro.Ministerio!=null)
            {
                vehiculosQueryable = vehiculosQueryable.Where(x => x.MinisterioId== filtro.Ministerio);
            }

            var lista = await vehiculosQueryable
                .Include(x => x.Modelo)
                .Include(x => x.Imagenes)
                .Where(x => x.Active)
                .OrderBy(x => x.Patente)
                .ToListAsync(); 
            return _mapper.Map<List<VehiculoDTO>>(lista);
        }

        [HttpGet("{id:long}", Name = "GetVehiculoById")]
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult<VehiculoDTO>> Get(int id)
        {
            var _currentUser = GetCurrenUser();
            return await Get<Vehiculo, VehiculoDTO>(id);
        }

        [HttpPost]
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult> Post(VehiculoCreacionDTO vehiculoCreacionDTO)
        {
            var _currentUser = GetCurrenUser();
            var vehiculoToSave = _mapper.Map<Vehiculo>(vehiculoCreacionDTO);
            SetAuditCreate(vehiculoToSave);
            _context.Vehiculos.Add(vehiculoToSave);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Vehiculo id: {vehiculoToSave.Id} creado por {_currentUser.Nombre}");
            var vehiculoDto = _mapper.Map<VehiculoDTO>(vehiculoToSave);
            return new CreatedAtRouteResult("GetVehiculoById", new { id = vehiculoToSave.Id }, vehiculoDto);
        }

        [HttpPost("otro")]
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult> PostOtro(VehiculoCreacionOtroDTO model)
        {
             var _currentUser = GetCurrenUser();
            var entityToSave = _mapper.Map<Vehiculo>(model);
            SetAuditCreate(entityToSave);
            _context.Vehiculos.Add(entityToSave);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Vehiculo id: {entityToSave.Id} creado por {_currentUser.Nombre}");
            var vehiculoDto = _mapper.Map<VehiculoDTO>(entityToSave);
            return new CreatedAtRouteResult("GetVehiculoById", new { id = entityToSave.Id }, vehiculoDto);
        }

        [HttpPut("{id:long}")]
        [ServiceFilter(typeof(MyActionFilter))]
        public async Task<ActionResult> Put(long id, [FromBody] VehiculoCreacionDTO vehiculoCreacionDTO)
        {
            var _currentUser = GetCurrenUser();
            var vehiculoFromDb = await _context.Vehiculos.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (vehiculoFromDb == null)
            {
                return BadRequest("No existe el vehiculo");
            }

            var vehiculoToUpdate = _mapper.Map<Vehiculo>(vehiculoCreacionDTO);
            vehiculoToUpdate.Id = id;
            vehiculoToUpdate.Active = true;
            vehiculoToUpdate.Version = vehiculoFromDb.Version;
            SetAuditUpdate(vehiculoToUpdate);
            _context.Entry(vehiculoToUpdate).State = EntityState.Modified;
            //var imagenListFromDb = _mapper.Map<List<Imagen>>(vehiculoCreacionDTO.Imagenes);
            var imagenToSave = new List<Imagen>();
            foreach (var image in vehiculoToUpdate.Imagenes)
            {
                if (image.Id == 0) {
                    image.VehiculoId = vehiculoToUpdate.Id;
                    imagenToSave.Add(image);
                }
                
                
            }
            _context.Imagenes.AddRange(imagenToSave);
            await _context.SaveChangesAsync();
            _logger.LogInformation($"Vehiculo id: {vehiculoToUpdate.Id} modificado por {_currentUser.Nombre}");
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<VehiculoPatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var entity = await _context.Vehiculos.FirstOrDefaultAsync(x => x.Id == id);

            var entityDTO = _mapper.Map<VehiculoPatchDTO>(entity);
            patchDocument.ApplyTo(entityDTO,ModelState);

            var isValid = TryValidateModel(entityDTO);
            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            SetAuditUpdate(entity);
            _mapper.Map(entityDTO, entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost("imagen")]
        public async Task<ActionResult<string>> Post([FromForm] IFormFile file)
        {
            if (file != null)
            {
                
                    var ruta = await  _fileStorage.SaveFile(file, Path.GetExtension(file.FileName), Container, file.ContentType);
                    return Ok(new ImagenResponseDTO { Url = ruta});

            }

            return NoContent();
        }

        [HttpDelete("imagen")]
        public async Task<ActionResult<string>> Delete([FromQuery] int id, [FromQuery] string url) 
        {
            var exist = await _context.Imagenes.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound();
            }

            _context.Remove(new Imagen() { Id = id });
            await _fileStorage.DeleteFile(url, Container);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
