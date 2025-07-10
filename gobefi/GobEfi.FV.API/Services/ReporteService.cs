using ClosedXML.Report;
using Dapper;
using GobEfi.FV.API.Models.DTOs;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API.Services
{
    public class ReporteService : IReporteService
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public ReporteService(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }
        public async Task<MemoryStream> ReporteVehiculos(long Id, bool isAdmin)
        {
            var template = new XLTemplate(@".\wwwroot\reportes\templates\vehiculos.xlsx");

            var data = await ReporteVehiculosData(Id, isAdmin);
            template.AddVariable(data);
            template.Generate();
            MemoryStream ms = new MemoryStream();
            template.SaveAs(ms);
            ms.Position = 0;

            return ms;
        }

        private async Task<ReporteVehiculosDTO> ReporteVehiculosData(long servicioId, bool isAdmin)
        {
            var strQuery= isAdmin ? "sp_reporte_vehiculos_all" : $"sp_reporte_vehiculos {servicioId}";
            var result = new ReporteVehiculosDTO();
            using (var connection = new SqlConnection(_connectionString)) {

                var lista = await connection.QueryAsync<VehiculosListaDTO>(strQuery).ConfigureAwait(false);
                result.Fecha = DateTime.Now;
                result.Vehiculos = lista.ToList();
                return result;
            }
        }   
    }


    
}
