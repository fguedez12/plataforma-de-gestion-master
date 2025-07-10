using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration;
using ClosedXML.Report;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.ReporteModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace GobEfi.Web.Services
{
    public class ReporteService : IReporteService
    {
        private readonly string _connectionString;
        private readonly IReporteRepository _repoReporte;
        private readonly IUsuarioRepository _repoUsuario;
        private readonly IReporteRolRepository _repoReporteRol;
        private readonly IDivisionRepository _repoDivision;
        private readonly IServicioRepository _repoServicio;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        private IHostingEnvironment _hostingEnvironment;
        private readonly ApplicationDbContext context;

        public ReporteService(IReporteRepository repoReporte,
        IUsuarioRepository repoUsuario,
        IReporteRolRepository repoReporteRol,
        IDivisionRepository repoDivision,
        IServicioRepository repoServicio,
        IMapper mapper,
        ILoggerFactory factory,
        IHostingEnvironment hostingEnvironment,
        Microsoft.Extensions.Configuration.IConfiguration configuration)
        {
            _repoReporte = repoReporte;
            _repoUsuario = repoUsuario;
            _repoReporteRol = repoReporteRol;
            _repoDivision = repoDivision;
            _repoServicio = repoServicio;
            _mapper = mapper;
            _logger = factory.CreateLogger<ReporteService>();
            _hostingEnvironment = hostingEnvironment;
            _connectionString = configuration.GetConnectionString("Default");
        }

        public ICollection<ReporteModel> GetByUser(string userId, long servicioId)
        {
            _logger.LogInformation($"Obteniendo los reportes del usuario: {userId}");
            
            var usuarioRoles = _repoUsuario.Query().Include(ur => ur.UsuarioRoles).FirstOrDefault(u => u.Id == userId).UsuarioRoles;
            //var servicioId =  _repoDivision.Get(long.Parse(divisionId)).ServicioId;
            
            var reportes = _repoReporteRol.Query().Where(rr => 
                usuarioRoles.Any(u => u.RoleId == rr.RolId)
            ).Select(rr => rr.Reporte);


            bool esPMG = _repoServicio.Get(servicioId).ReportaPMG;

            var ListaReportes = reportes.Where(r => !r.EsPMG && r.Activo).Include(t => t.TipoArchivo).ToList();

            if (esPMG)
            {
                //var reportesPMG = _repoReporte.Query().Include(t => t.TipoArchivo).Where(r => r.EsPMG && r.Activo).ToList();
                var reportesPMG = reportes.Where(r => r.EsPMG && r.Activo).Include(t => t.TipoArchivo).ToList();

                foreach (var reportePMG in reportesPMG)
                {
                    if (!ListaReportes.Exists(r => r.Id == reportePMG.Id))
                        ListaReportes.Add(reportePMG);
                }
            }

            foreach (var item in ListaReportes)
            {
                if (!string.IsNullOrEmpty(item.RutaDondeObtenerArchivo))
                    item.RutaDondeObtenerArchivo = item.RutaDondeObtenerArchivo + servicioId; //_repoDivision.Get(long.Parse(divisionId)).ServicioId;
            }

            return _mapper.Map<List<ReporteModel>>(ListaReportes);
        }

        public IEnumerable<ReporteModel> All()
        {
            throw new System.NotImplementedException();
        }

        public void Delete(long id)
        {
            throw new System.NotImplementedException();
        }

        public ReporteModel Get(long id)
        {
            var reporte = _repoReporte.Query().Include(t => t.TipoArchivo).Where(r => r.Id == id).FirstOrDefault();
            return _mapper.Map<ReporteModel>(reporte);
        }

        public long Insert(ReporteModel model)
        {
            throw new System.NotImplementedException();
        }

        public void Update(ReporteModel model)
        {
            throw new System.NotImplementedException();
        }

        // ejemplo https://www.talkingdotnet.com/import-export-excel-asp-net-core-2-razor-pages/
        public async Task<MemoryStream> ExportarExcel(long servicioId, long reporteId, string sFileName, bool isAdmin = false)
        {
            
            string sWebRootFolder = _hostingEnvironment.WebRootPath;
            // string sFileName = @"demo.xlsx";
            // string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, sFileName);
            FileInfo file = new FileInfo(Path.Combine(sWebRootFolder, sFileName));
            MemoryStream memory = new MemoryStream();
            
            using (var fs = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Create, FileAccess.Write))
            {
                IWorkbook workbook;
                workbook = new XSSFWorkbook();
                ISheet excelSheet = workbook.CreateSheet("Demo");
                IRow row = excelSheet.CreateRow(0);
                

                row.CreateCell(0).SetCellValue("Listado de Gestores y Edificio");
                
                var data = _repoReporte.ObtenerData(servicioId, isAdmin);
                var items = ((IEnumerable) data).Cast<object>().ToList();// _repoReporte.Query().ToList();

                string[] headers = new[] { "Ministerio/Institución", "Servicio", "Unidad Id", "Región", "Comuna", "Superficie", "Gestor Energético", "Tipo Gestor","Correo Electrónico"  };
                string[] columns = new[] { "InstitucionNombre", "ServicioNombre", "DivisionId", "RegionNombre", "ComunaNombre", "DivisionSuperficie", "UsuarioNombre", "RolNombre", "UsuarioEmail" };
                int[] size = new[] { 8000,4000,4400,5000,6000,4550,5500,5506,5005 };

                //create header
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = row.CreateCell(i);
                    cell.SetCellValue(headers[i]);
                    
                    excelSheet.SetColumnWidth(i, size[i]);
                }

                //fill content 
                for (int i = 0; i < items.Count; i++)
                {
                    var rowIndex = i + 1;
                    row = excelSheet.CreateRow(rowIndex);
                    for (int j = 0; j < columns.Length; j++)
                    {
                        try
                        {
                            var cell = row.CreateCell(j);
                            
                            cell.SetCellValue(items[i].GetType().GetProperty(columns[j]).GetValue(items[i], null).ToString());
                            
                        }
                        catch (System.Exception)
                        {

                            throw;
                        }
                        
                    }
                }

                workbook.Write(fs);
            };

            using (var stream = new FileStream(Path.Combine(sWebRootFolder, sFileName), FileMode.Open))
            {
                await stream.CopyToAsync(memory);
            };

            memory.Position = 0;

            return memory;
        }

        public async Task InsertAsync(ReporteModel reporte)
        {
            var buscandoReporteById = await _repoReporte.Query().Where(r => r.Id == reporte.Id).FirstOrDefaultAsync();

            if (buscandoReporteById != null)
                throw new Exception("reporte ya existe");


            var report = _mapper.Map<Reporte>(reporte);

            report.TipoArchivoId = 5;

            _repoReporte.Insert(report);
            _repoReporte.SaveChanges();
        }

        public async Task UpdateAsync(ReporteModel reporte)
        {
            var reporteOriginal = await _repoReporte.All().Where(r => r.Id == reporte.Id).FirstOrDefaultAsync();

            if (reporteOriginal == null)
            {
                throw new NotFoundException(nameof(reporteOriginal));
            }

            reporteOriginal.Nombre = reporte.Nombre;

            _repoReporte.Update(reporteOriginal);
            _repoReporte.SaveChanges();

        }

        public async Task<MemoryStream> DisenioPasivoReporte(long servicioId)
        {
            try
            {
               
                //const string outputFile = @".\Reportes\DisenioPasivo\report.xlsx";
                var template = new XLTemplate(@".\wwwroot\Reportes\Templates\DisenioPasivoTemplate.xlsx");
                await DpReportPaso0(servicioId);
                var data =await  DpReportData(servicioId);

                data.Edificios = await DpReportEdificioData(servicioId);
                data.Unidades = await DpReporteUnidadesData(servicioId);
                template.AddVariable(data);
                template.Generate();

                MemoryStream ms = new MemoryStream();
                template.SaveAs(ms);
                ms.Position = 0;
                
                return ms;
            }
            catch (Exception ex )
            {

                throw new Exception(ex.Message);
            }
            
        }


        private async Task<DisenioPasivoReportModel> DpReportData(long servicioId)
        {
            var result = new DisenioPasivoReportModel();

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_REPORTE_DP", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Id", servicioId));
                    
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            result.NumeroEdificios = Convert.ToInt32(reader["NumeroEdificios"]);
                            result.Servicio = reader["Servicio"].ToString();
                            result.Fecha = DateTime.Now.ToString("dd-MM-yyyy");
                        }
                    }
                    return result;
                }
            }

        }

        private async Task DpReportPaso0(long servicioId)
        {
            

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_REPORTE_DP_0", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Id", servicioId));

                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }

        }

        private async Task<List<DpReporteEdificioModel>> DpReportEdificioData(long servicioId)
        {

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_REPORTE_DP_EDIFICIOS",sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Id", servicioId));
                    var result = new List<DpReporteEdificioModel>();
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(MapDpRepo(reader));
                        }
                    }
                    return result;
                }
            }
        }

        private async Task<List<DpReporteUnidadesDetalle>> DpReporteUnidadesData(long servicioId)
        {

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_REPORTE_DP_UNIDADES_DETALLE", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Id", servicioId));
                    var result = new List<DpReporteUnidadesDetalle>();
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            result.Add(MapDpRepoUnidades(reader));
                        }
                    }
                    return result;
                }
            }
        }

        private async Task DpUpdateReporte(long servicioId)
        {

            using (SqlConnection sql = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand("SP_REPORTE_DP_UPDATE", sql))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("Id", servicioId));
                    var result = new List<DpReporteUnidadesDetalle>();
                    await sql.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private DpReporteUnidadesDetalle MapDpRepoUnidades(SqlDataReader reader)
        {
            return new DpReporteUnidadesDetalle()
            {
                IdUnidad = Convert.ToInt64(reader["IdUnidad"]),
                DireccionUnidad = reader["DireccionUnidad"].ToString(),
                IdEdificio = Convert.ToInt64(reader["IdEdificio"]),
                Region = reader["Region"].ToString(),
                Comuna = reader["Comuna"].ToString(),
                AnioConstruccion = Convert.ToInt32(reader["AnioConstruccion"]),
                TipoEdificio = reader["TipoEdificio"].ToString(),
                TipoPropiedad = reader["TipoPropiedad"].ToString(),
                SuperficieTotal = Convert.ToDecimal(reader["SuperficieTotal"]),
                Latitud = Convert.ToDecimal(reader["Latitud"]),
                Longitud = Convert.ToDecimal(reader["Longitud"]),
                TipoAgrupamiento = reader["TipoAgrupamiento"].ToString(),
                Entorno = reader["Entorno"].ToString(),
                NumeroPisos = Convert.ToInt32(reader["NumeroPisos"]),
                MurosIncompletos = reader["MurosIncompletos"].ToString(),
                SuperficiePromedio = Convert.ToDecimal(reader["SuperficiePromedio"]),
                AlturaPromedio = Convert.ToDecimal(reader["AlturaPromedio"]),
                SuperficieTotalPisos = Convert.ToDecimal(reader["SuperficieTotalPisos"]),
                AlturaTotal = Convert.ToDecimal(reader["AlturaTotal"]),
                VolumenTotal = Convert.ToDecimal(reader["VolumenTotal"]),
                NumeroMuros = Convert.ToInt32(reader["NumeroMuros"]),
                LargoMurosNorte = Convert.ToDecimal(reader["LargoMurosNorte"]),
                LargoMurosEste = Convert.ToDecimal(reader["LargoMurosEste"]),
                LargoMurosSur = Convert.ToDecimal(reader["LargoMurosSur"]),
                LargoMurosOeste = Convert.ToDecimal(reader["LargoMurosOeste"]),
                SolucionContructivaPiso = reader["SolucionContructivaPiso"].ToString(),
                EspesorAislacionPiso = reader["EspesorAislacionPiso"].ToString(),
                TipoAislacionPiso = reader["TipoAislacionPiso"].ToString(),
                SolucionContructivaMuro = reader["SolucionContructivaMuro"].ToString(),
                AislacionMuro = reader["AislacionMuro"].ToString(),
                EspesorMuro = reader["EspesorMuro"].ToString(),
                SolucionConstructivaVentanas = reader["SolucionConstructivaVentanas"].ToString(),
                TipoCierreVentanas = reader["TipoCierreVentanas"].ToString(),
                TipoMarcoVentanas = reader["TipoMarcoVentanas"].ToString(),
                PorcentajeVentana = reader["PorcentajeVentana"].ToString(),
                TipoPuertas = reader["TipoPuertas"].ToString(),
                TipoMarcoPuertas = reader["TipoMarcoPuertas"].ToString(),
                SuperficiePuertas= reader["SuperficiePuertas"].ToString(),
                SolucionConstructivaTechos = reader["SolucionConstructivaTechos"].ToString(),
                TipoAislacionTechos = reader["TipoAislacionTechos"].ToString(),
                EspesorAislacionTechos = reader["EspesorAislacionTechos"].ToString(),
                SolucionConstructivaCimiento = reader["SolucionConstructivaCimiento"].ToString(),
                NFotoEnvolventes = Convert.ToInt32(reader["NFotoEnvolventes"]),
                NFotoDetalles = Convert.ToInt32(reader["NFotoDetalles"]),
                NFotoProblemas = Convert.ToInt32(reader["NFotoProblemas"]),
                NFotoArquitectura = Convert.ToInt32(reader["NFotoArquitectura"]),
                NFotoElevaciones = Convert.ToInt32(reader["NFotoElevaciones"]),
                NFotoEstructurales = Convert.ToInt32(reader["NFotoEstructurales"]),
                NFotoEspecialidad = Convert.ToInt32(reader["NFotoEspecialidad"]),
            };
        }

        private DpReporteEdificioModel MapDpRepo(SqlDataReader reader)
        {
            

            return new DpReporteEdificioModel()
            {
                Id = Convert.ToInt32(reader["id"]),
                Direccion = reader["Direccion"].ToString(),
                Region = reader["Region"].ToString(),
                Comuna = reader["Comuna"].ToString(),
                Servicio = reader["Servicio"].ToString(),
                DpP1 = reader["DpP1"].ToString(),
                DpP2 = reader["DpP2"].ToString(),
                DpP3 = reader["DpP3"].ToString(),
                DpP4 = reader["DpP4"].ToString()
            };

            
        }
    }
}