using api_gestiona.DTOs.Sistemas;
using api_gestiona.Entities;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Services
{
    public class SistemasService : ISistemasService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public SistemasService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public async Task<SistemasDataDTO> GetData(long divisionId) 
        { 
            var unidad = await _context.Divisiones.FirstAsync(x=>x.Id == divisionId) ?? throw new Exception("El Recurso no existe");
            var resp = _mapper.Map<SistemasDataDTO>(unidad);
            var luminarias = await _context.TiposLuminarias.ToListAsync();
            var equiposCalefaccion = await _context.TiposEquiposCalefaccion.Where(x=>x.CA).ToListAsync();
            var equiposRefrigeracion = await _context.TiposEquiposCalefaccion.Where(x => x.FR).ToListAsync();
            var equiposAc = await _context.TiposEquiposCalefaccion.Where(x => x.AC).ToListAsync();
            resp.Luminarias = _mapper.Map<List<LuminariasListDTO>>(luminarias);
            resp.EquiposCalefaccion = _mapper.Map<List<EquiposCalefaccionListDTO>>(equiposCalefaccion);
            resp.EquiposRefrigeracion = _mapper.Map<List<EquiposCalefaccionListDTO>>(equiposRefrigeracion);
            resp.EquiposAc = _mapper.Map<List<EquiposCalefaccionListDTO>>(equiposAc);
            return resp;
        }

        public async Task<List<EnergeticoEquipoListDTO>> GetEnergeticoEquipo(long equipoId)
        {
            var equipo = await _context.TiposEquiposCalefaccion
                .Where(x => x.Id == equipoId)
                .Include(x => x.TipoEquipoCalefaccionEnergeticos)
                .ThenInclude(tece => tece.Energetico)
                .FirstOrDefaultAsync();

            if (equipo == null)
            {
                throw new Exception("El recurso no exite");
            }

            var energeticoList = equipo?.TipoEquipoCalefaccionEnergeticos?
                     .Select(tece => tece.Energetico)
                     .ToList();
            return _mapper.Map<List<EnergeticoEquipoListDTO>>(energeticoList);

        }

        public async Task<List<TipoColectorListDTO>> GetTiposColectores(string tipo)
        {
            var colectore = await _context.TiposColectores.Where(x=>x.Tipo == null || x.Tipo==tipo).ToListAsync();
            var resp = _mapper.Map<List<TipoColectorListDTO>>(colectore);
            return resp;
        }

        public async Task SaveSistemaData(long divisionId, string userId, SistemasDataDTO model)
        {
            var division = await _context.Divisiones.FirstOrDefaultAsync(x => x.Id == divisionId) ?? throw new Exception("No se encuentra el recurso");
            _mapper.Map(model,division);
            division.UpdatedAt = DateTime.Now;
            division.ModifiedBy = userId;
            division.Version = division.Version + 1;
            _context.Divisiones.Update(division);
            await _context.SaveChangesAsync();
        }
    }
}
