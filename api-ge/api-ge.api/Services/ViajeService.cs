using api_gestiona.DTOs.Viajes;
using api_gestiona.Entities;
using api_gestiona.Services.Contracts;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Services
{
    public class ViajeService: BaseService, IViajeService 
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ViajeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task CreateViaje(string userId,long servicioId, ViajeCreateDTO model, int year)
        {
            var entity = _mapper.Map<Viaje>(model);
            entity.ServicioId = servicioId;
            entity.Year = year;
            entity = SetAuditableSave(entity, userId);
            try
            {
                entity.Distancia = await CalcularDistancia(entity);
            }
            catch (Exception)
            {

                throw new Exception("Problema al calcular la distancia");
            }
            _context.Viajes.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateViaje(string userId, long viajeId, ViajeCreateDTO model,int year )
        {
            var entity = await _context.Viajes
                .FirstOrDefaultAsync(x=>x.Id == viajeId) ?? throw new Exception("El recurso solicitado no existe");

            var mapEntity = MapDataViaje(entity, model);
            mapEntity = SetAuditableUpdate(mapEntity, userId, mapEntity);
            mapEntity.Year = year;
            try
            {
                entity.Distancia = await CalcularDistancia(entity);
            }
            catch (Exception)
            {

                throw new Exception("Problema al calcular la distancia");
            }
            await _context.SaveChangesAsync();
        }

        private Viaje MapDataViaje(Viaje viaje, ViajeCreateDTO model)
        {
            viaje.Periodo = model.Periodo;
            if(!string.IsNullOrEmpty(model.CiudadOrigen)) 
            {
                viaje.CiudadOrigen = model.CiudadOrigen;
            }

            if(!string.IsNullOrEmpty(model.CiudadDestino)) 
            {
                viaje.CiudadDestino = model.CiudadDestino;
            }

            viaje.NViajesSoloIdaRetorno = model.NViajesSoloIdaRetorno;
            viaje.NViajesIdaVuelta = model.NViajesIdaVuelta;
            if (model.AeropuertoOrigenId != null)
            {
                viaje.AeropuertoOrigenId = model.AeropuertoOrigenId;
            }
            if (model.AeropuertoDestinoId != null) 
            {
                viaje.AeropuertoDestinoId = model.AeropuertoDestinoId;
            }
            return viaje;
        }

        private  async Task<int> CalcularDistancia(Viaje viaje)
        {
            var aeropueroIni = await _context.Aeropuertos
                .FirstOrDefaultAsync(x=>x.Id == viaje.AeropuertoOrigenId) ?? throw new Exception("El recurso no existe");
            
            var aeropueroFin = await _context.Aeropuertos
                .FirstOrDefaultAsync(x => x.Id == viaje.AeropuertoDestinoId)?? throw new Exception("El recurso no existe");
            double R = 6371.0;

            var lat1 = ToRadians(aeropueroIni.Lat);
            var lon1 = ToRadians(aeropueroIni.Lon);
            var lat2 = ToRadians(aeropueroFin.Lat);
            var lon2 = ToRadians(aeropueroFin.Lon);

            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;

            double a = Math.Sin(dlat / 2) * Math.Sin(dlat / 2) + Math.Cos(lat1) * Math.Cos(lat2) * Math.Sin(dlon / 2) * Math.Sin(dlon / 2);
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            double distancia = R * c;
            return (int)distancia;
        }

        private static double ToRadians(double degrees)
        {
            return degrees * (Math.PI / 180);
        }
    }
}
