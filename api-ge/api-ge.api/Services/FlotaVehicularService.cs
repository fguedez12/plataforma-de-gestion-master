using api_gestiona.DTOs.Divisiones;
using api_gestiona.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.Graph.Constants;

namespace api_gestiona.Services
{
    public class FlotaVehicularService : IFlotaVehicularService
    {
        private readonly string URL;
        private readonly ApplicationDbContext _context;

        public FlotaVehicularService(IConfiguration configuration, ApplicationDbContext context)
        {
            URL = configuration.GetValue<string>("ApiConfiguration:apiFlota");
            _context = context;
        }

        public async Task<List<VehiculoDTO>> GetVehiculosServicioByDivisionId(long divisionId)
        {
            var servicioId = await _context.Divisiones.Where(x => x.Id == divisionId).Select(x => x.Servicio.Id).FirstOrDefaultAsync();
            if(servicioId == 0) 
            {
                throw new NullReferenceException("No existe la unidad");
            }    
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{URL}/api/vehiculos/byServicioId/{servicioId}");
            var list = new List<VehiculoDTO>();
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<List<VehiculoDTO>>();
            }

            return list;
        }


    }
}
