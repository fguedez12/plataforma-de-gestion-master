using AutoMapper;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Contracts.Repositories;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Core.Extensions;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.CompraModels;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Services.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Services
{
    public class CompraService : ICompraService
    {
        private readonly IMapper _mapper;
        private readonly ICompraRepository _repoCompra;
        private readonly ITipoArchivoRepository _repoTipoArchivo;
        private readonly IEnergeticoUnidadMedidaRepository _repoUMedida;
        private readonly ICompraMedidorRepository _compraMedidorRepo;
        private readonly IMedidorDivisionRepository _repoMedidorDivision;
        private readonly IDivisionRepository _repoDivision;
        private readonly IUsuarioDivisionRepository _repoUsuarioDivision;
        private readonly IEnergeticoDivisionRepository _repoEnergeticoDivision;
        private readonly IParametroMedicionRepository _repoParametroMedicion;
        private readonly IUnidadMedidaRepository _repoUnidadMedida;
        private readonly UserManager<Usuario> _userManager;
        private readonly Usuario _currentUser;
        private readonly ILogger _logger;

        public CompraService(ILoggerFactory loggerFactory
        , IMapper mapper
        , ICompraRepository repoCompra
        , ITipoArchivoRepository repoTipoArchivo
        , IEnergeticoUnidadMedidaRepository repoUMedida
        , ICompraMedidorRepository compraMedidorRepo
        , IMedidorDivisionRepository repoMedidorDivision
        , IDivisionRepository repoDivision
        , IUsuarioDivisionRepository repoUsuarioDivision
        , IEnergeticoDivisionRepository repoEnergeticoDivision
        , IUnidadMedidaRepository repoUnidadMedida
        , IParametroMedicionRepository repoParametroMedicion
        , UserManager<Usuario> userManager
        , IHttpContextAccessor httpContextAccessor)
        {
            _logger = loggerFactory.CreateLogger<CompraService>();
            _mapper = mapper;
            _repoCompra = repoCompra;
            _repoUMedida = repoUMedida;
            _compraMedidorRepo = compraMedidorRepo;
            _repoMedidorDivision = repoMedidorDivision;
            _repoDivision = repoDivision;
            _repoUsuarioDivision = repoUsuarioDivision;
            _userManager = userManager;
            _currentUser = (_userManager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
            _repoTipoArchivo = repoTipoArchivo;
            _repoEnergeticoDivision = repoEnergeticoDivision;
            _repoParametroMedicion = repoParametroMedicion;
            _repoUnidadMedida = repoUnidadMedida;
        }

        public IEnumerable<CompraModel> All()
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAsync(long id)
        {
            var compra = await _repoCompra.All().Where(c => c.Id == id).FirstOrDefaultAsync();
            if (compra == null)
            {
                return -1;
            }

            compra.Active = false;
            _repoCompra.Delete(compra);
            var result = _repoCompra.SaveChanges();

            _logger.LogInformation($"Compra [{id}] desactivada por el usuario [{_currentUser.Id}]");

            return result;
        }

        public CompraModel Get(long id)
        {
            var fromDb = _repoCompra.Query().Where(c => c.Id == id).FirstOrDefault();


            return _mapper.Map<CompraModel>(fromDb);
        }

        public async Task<long> Add<T>(T entity) where T : class
        {

            var compraToSave = _mapper.Map<Compra>(entity);

            if (compraToSave.CompraMedidor.Count == 0 && compraToSave.UnidadMedidaId == null)
            {
                compraToSave.UnidadMedidaId = _repoUMedida.Query().Where(u => u.EnergeticoId == compraToSave.EnergeticoId).FirstOrDefault().UnidadMedidaId;
            }

            compraToSave.CreatedAt = DateTime.Now;
            compraToSave.CreatedBy = _currentUser.Id;
            compraToSave.Active = true;
            compraToSave.Version = 1;
            compraToSave.Consumo = compraToSave.CompraMedidor.Count > 0 ? compraToSave.CompraMedidor.Sum(c => c.Consumo) : compraToSave.Consumo;
            compraToSave.CreatedByDivisionId = compraToSave.DivisionId;
            _repoCompra.Add(compraToSave);

             await _repoCompra.SaveAll();

             _logger.LogInformation($"Compra [{compraToSave.Id}] creada por el usuario [{_currentUser.Id}]");

            if (compraToSave.Id < 0) {
                throw new Exception("Error al ingresar nueva compra, por favor revise los datos ingresados.");
            }


            return compraToSave.Id;
        }

        public long Insert(CompraModel model)
        {
            throw new NotImplementedException();
        }

        public void Update(CompraModel model)
        {
            throw new NotImplementedException();
        }
        
        public async Task<long> Update(long id, CompraForEdit compraFromBody)
        {
            if (id != compraFromBody.Id) {

                throw new Exception("El id no corresponde");
            }


            Compra compraOriginal = _repoCompra.Query().Where(c => c.Id == id).Include(c=>c.CompraMedidor).FirstOrDefault();

            //hl:Cambio para las compras sin medidor
            if (compraFromBody.FechaCompra == null) {
                compraFromBody.FechaCompra = compraOriginal.FechaCompra.ToString();
            }

            if (compraOriginal == null)
                return -1;


            _compraMedidorRepo.EliminarSegunCompraId(id);

            var compra = _mapper.Map<Compra>(compraFromBody);

            foreach (var c in compraOriginal.CompraMedidor)
            {
                if(!compra.CompraMedidor.Any(x=>x.MedidorId == c.MedidorId))
                {
                    c.Id = 0;
                    compra.CompraMedidor.Add(c);
                }
            }

            compra.Consumo = compraFromBody.ConsumoCompra > 0 ? compraFromBody.ConsumoCompra : compra.CompraMedidor.Sum(c => c.Consumo);
            compra.Active = true;
            compra.UpdatedAt = DateTime.Now;
            compra.Version = ++compraOriginal.Version;
            compra.CreatedAt = compraOriginal.CreatedAt;
            compra.ModifiedBy = _currentUser.Id;
            compra.EstadoValidacionId = "sin_revision";
            compra.CreatedBy = compraOriginal.CreatedBy;

            if (compra.CompraMedidor.Count == 0 && compra.UnidadMedidaId == null)
            {
                compra.UnidadMedidaId = _repoUMedida.Query()
                            .Where(u => u.EnergeticoId == compra.EnergeticoId)
                            .FirstOrDefault().UnidadMedidaId;
            }
            compra.CreatedByDivisionId = compraOriginal.CreatedByDivisionId;

            

            _repoCompra.Update(compra);

            try
            {
                await _repoCompra.SaveAll();

                _logger.LogInformation($"Compra [{compra.Id}] actualizada por el usuario [{_currentUser.Id}]");

            }
            catch (Exception ex)
            {
                return -2;
            }

            return compra.Id;
        }

        public void Delete(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ValidaPermiso(long divisionId)
        {
            bool isAdmin = await _userManager.IsInRoleAsync(_currentUser, Constants.Claims.ES_ADMINISTRADOR);

            if (isAdmin)
                return true;

            Division division = _repoDivision.Query().Where(d => d.Id == divisionId).Include(d => d.UsuariosDivisiones).FirstOrDefault();

            return division.UsuariosDivisiones.Any(ud => ud.UsuarioId == _currentUser.Id);
        }

        public async Task<IEnumerable<CompraParaValidarModel>> getComprasParaValidar(CompraParaValidarRequest request)
        {
            bool isAdmin = await _userManager.IsInRoleAsync(_currentUser, Constants.Claims.ES_ADMINISTRADOR);
            var usuarioDivision = _repoUsuarioDivision.Query().Where(ud => ud.UsuarioId == _currentUser.Id);

            //var queryCompra = _repoCompra.Query().Where(c => isAdmin ? true : usuarioDivision.Any(ud => ud.DivisionId == c.DivisionId));
            var medidoresDivisiones = _repoMedidorDivision.Query().Where(md => usuarioDivision.Any(ud => ud.DivisionId == md.DivisionId) && md.Active==true).ToList();
            var compraMedidor = _compraMedidorRepo.Query().Where(cm => medidoresDivisiones.Any(md => md.MedidorId == cm.MedidorId)).ToList();
            IQueryable<Compra> queryCompra;

            if (request.CompraId > 0)
            {
                
                queryCompra = _repoCompra.Query().Where(c => isAdmin ? true : usuarioDivision.Any(ud => ud.DivisionId == c.DivisionId) && c.Active == true).Where(c => c.Division.Active == true);
                request.InstitucionId = 0;
                request.ServicioId = 0;
              
            }
            else 
            {
                //HL 23/09/2019
                //Se muestran todas las compras para el perfil Adm
                 queryCompra = _repoCompra.Query().Where(c => isAdmin ? true : compraMedidor.Any(cm => cm.CompraId == c.Id) && c.Active == true).Where(c => c.Division.Active == true);
                //Fin cambio
            }


            if (request.InstitucionId > 0 || request.ServicioId > 0)
            {
                queryCompra = queryCompra.Include(c => c.Division).Include(c => c.Division.Servicio);
            }

            if (request.InstitucionId > 0)
            {
                queryCompra = queryCompra.Where(c => c.Division.Servicio.InstitucionId == request.InstitucionId);
            }

            if (request.ServicioId > 0)
            {
                queryCompra = queryCompra.Where(c => c.Division.ServicioId == request.ServicioId);
            }

            if (request.RegionId > 0)
            {
                queryCompra = queryCompra.Include(c => c.Division.Edificio.Comuna);
                queryCompra = queryCompra.Where(c => c.Division.Edificio.Comuna.RegionId == request.RegionId);
            }

            if (request.EdificioId > 0)
            {
                queryCompra = queryCompra.Include(c => c.Division);
                queryCompra = queryCompra.Where(c => c.Division.EdificioId == request.EdificioId);
            }

            if (request.DivisionId > 0)
            {
                medidoresDivisiones = _repoMedidorDivision.Query().Where(md => md.DivisionId == request.DivisionId && md.Active==true).ToList();
                compraMedidor = _compraMedidorRepo.Query().Where(cm => medidoresDivisiones.Any(md => md.MedidorId == cm.MedidorId)).ToList();
                //queryCompra = _repoCompra.Query().Where(c => isAdmin ? true : compraMedidor.Any(cm => cm.CompraId == c.Id));
                

                queryCompra = queryCompra.Where(c => compraMedidor.Any(cm => cm.CompraId == c.Id) && c.Active == true);
            }

            if (request.EnergeticoId > 0)
            {
                //queryCompra = queryCompra.Include(c => c.EnergeticoId);
                queryCompra = queryCompra.Where(c => c.EnergeticoId == request.EnergeticoId);
            }

            if (request.NumClienteId > 0)
            {
                //queryCompra = queryCompra.Include(c => c.EnergeticoId);
                queryCompra = queryCompra.Where(c => c.NumeroClienteId == request.NumClienteId);
            }

            if (request.NumMedidorId > 0)
            {
                queryCompra = queryCompra.Include(c => c.CompraMedidor);
                queryCompra = queryCompra.Where(c => c.CompraMedidor.Any(cm => cm.MedidorId == request.NumMedidorId));
            }

            if (request.CompraId > 0)
            {
                queryCompra = queryCompra.Where(c => c.Id == request.CompraId);
            }

            if (request.CompraId == 0)
            {
                //HL 24/06/2019
                //Aplica filtro Unidad PMG y filtro estado
                queryCompra = queryCompra.Where(c => c.Division.ReportaPMG == request.UnidadPmgId);
            }
           
          
            if (request.EstadoId != null)
            {
                queryCompra = queryCompra.Where(c => c.EstadoValidacionId == request.EstadoId);
            }
            //Fin cambio

            queryCompra = queryCompra.Where(c => c.Active); // JCP 06/08/2019 solo compras activas
            queryCompra = queryCompra.Include(c => c.Energetico);
            queryCompra = queryCompra.Include(c => c.NumeroCliente);
            queryCompra = queryCompra.Include(c => c.EstadoValidacion);

            List<CompraParaValidarModel> compras = new List<CompraParaValidarModel>();
            compras = await queryCompra.Select(c => new CompraParaValidarModel
            {
                Id = c.Id,
                InicioDeLectura = c.InicioLectura.ToShortDateString(),
                Unidad = c.Division.Direccion,
                Energetico = c.Energetico.Nombre,
                NumCliente = c.NumeroCliente.Numero,
                Estado = c.EstadoValidacion.Nombre,
                EstadoId = c.EstadoValidacion.Id,
                //RevisadoPor = GetNombreAppellido(c.RevisadoPor).Result,
                RevisadoPor = c.RevisadoPor,
            }).ToListAsync().ConfigureAwait(false);


            foreach (var itemCompra in compras)
            {
                itemCompra.RevisadoPor = GetNombreAppellido(itemCompra.RevisadoPor).Result;
            }

            return compras;
        }


        private async Task<string> GetNombreAppellido(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                return "";

            var usuario = await _userManager.FindByIdAsync(userId.Trim());

            return usuario.Nombres + " " + usuario.Apellidos ;
        }

        public async Task<CompraForEdit> GetParaRetornar(long compraId)
        {

            var compra = await _repoCompra.Query().Where(c => c.Id == compraId && c.Active)
               .Include(m => m.CompraMedidor)
               .Include(u => u.UnidadMedida)
               .Include(c => c.EstadoValidacion)
               .FirstOrDefaultAsync();

            var compraParaRetornar = _mapper.Map<CompraForEdit>(compra);



            return compraParaRetornar;
        }


        public  List<CompraTablaEnergetico> ObtenerLasCompras(long divisionId, List<EnergeticoActivoModel> energeticos, int? anioFiltro)
        {
            //validar si la unidad tiene numeros de clientes asociados a los medidores obtenidos
            var NumClientes = _repoEnergeticoDivision.Query().Where(ed => ed.DivisionId == divisionId && ed.Active && ed.NumeroClienteId != null).Include(ed => ed.NumeroCliente).Select(ed => ed.NumeroCliente).ToList();

            //obtener compras por relacion medidor division
            var medidores = _repoMedidorDivision.Query().Where(md => md.DivisionId == divisionId && md.Active).Select(md => md.Medidor);
            var medidoresDeDivision = medidores.Where(m => NumClientes.Any(nc => nc.Id == m.NumeroClienteId)).Select(m => m.Id).ToList();

            var consumoMedidor = _compraMedidorRepo.Query().Where(cm => medidoresDeDivision.Any(medidorId => medidorId == cm.MedidorId));


            var comprasMedidorCompartido = consumoMedidor.Select(cm => cm.Compra);
            comprasMedidorCompartido = comprasMedidorCompartido.Where(c => c.Active && energeticos.Exists(e => e.Id == c.EnergeticoId)).Distinct();

            //Solo las del año actual
            if (anioFiltro == null)
            {
                anioFiltro = DateTime.Now.Year;
            }
            comprasMedidorCompartido = comprasMedidorCompartido.Where(x => x.FechaCompra.Year == anioFiltro || x.FinLectura.Year==anioFiltro);

            var listaComprasCompartidas = comprasMedidorCompartido.ProjectTo<CompraTablaEnergetico>(_mapper.ConfigurationProvider).Distinct().ToList();

            

            foreach (var compra in listaComprasCompartidas) {

                compra.Consumo = consumoMedidor.Where(cm => cm.CompraId == compra.Id).Sum(c => c.Consumo);
            }

            List<CompraTablaEnergetico> toReturn = new List<CompraTablaEnergetico>();

            var comprasSinMedidor = _repoCompra.Query().Where(c => c.DivisionId == divisionId && c.NumeroClienteId == null && c.Active && energeticos.Exists(e=>e.Id == c.EnergeticoId) && c.FechaCompra.Year == anioFiltro);

            listaComprasCompartidas.AddRange(comprasSinMedidor.ProjectTo<CompraTablaEnergetico>(_mapper.ConfigurationProvider));

            foreach (var itemComprasTEnergetico in listaComprasCompartidas)
            {
                if (toReturn.Any(c => c.Id == itemComprasTEnergetico.Id))
                {
                    continue;
                }

                toReturn.Add(itemComprasTEnergetico);
            }

            int indexCompra = 0;

            foreach (var compra in toReturn)
            {


                List<CompraMedidor> medidoresFromDb = _compraMedidorRepo.Query().Where(c => c.CompraId == compra.Id).ToList();
                string numeroToReturn = string.Empty;
                string abrv = "";

                UnidadMedida unidadMedida = new UnidadMedida();

                if (medidoresFromDb.Count > 0)
                {

                    if (medidoresFromDb.FirstOrDefault().ParametroMedicionId != null)
                    {
                        unidadMedida = _repoParametroMedicion.Query()
                                    .Include(u => u.UnidadesMedida)
                                    .FirstOrDefault(p => p.Id == medidoresFromDb.FirstOrDefault().ParametroMedicionId)
                                    .UnidadesMedida;

                        abrv = unidadMedida.Abrv;
                    }



                    if (medidoresFromDb.FirstOrDefault().UnidadMedidaId != null)
                    {
                        unidadMedida = _repoUnidadMedida.Query().Where(u => u.Id == medidoresFromDb.FirstOrDefault().UnidadMedidaId).FirstOrDefault();

                        abrv = unidadMedida.Abrv;
                    }

                }

                if (unidadMedida.Id == 0)
                    unidadMedida.Id = compra.UnidadMedidaId;

                var enerUMedida = _repoUMedida.Query().Where(eu => eu.EnergeticoId == compra.EnergeticoId && eu.UnidadMedidaId == unidadMedida.Id).FirstOrDefault();

                if (enerUMedida == null)
                    throw new Exception($"La compra Id {compra.Id} no tiene asignado una unidad de medida, necesaria para obtener el factor de conversion correspondiente, favor contacte a su administrador.");

                compra.FactorDeConversion = enerUMedida.Factor;

                if (compra.Abrev == null)
                    compra.Abrev = abrv._toString();

                if (compra.FinLectura != DateTime.MinValue && compra.InicioLectura != DateTime.MinValue)
                {
                    // 1,97 * 30 [dias] = [59,1 px] aprox
                    compra.Ancho = (compra.FinLectura.AddDays(-1).Subtract(compra.InicioLectura)).TotalDays * 1.9;
                    compra.MarginIzq = compra.InicioLectura.Day * 1.9;

                    // Ej: the diff between 31/12/2010 and 01/01/2011 is 1
                    double meses = GetMonthDifference(compra.InicioLectura, compra.FinLectura);
                    DateTime fecha = compra.InicioLectura;
                    var cantidadPorMes = new List<CantidadPorMesClass>();
                    for (int i = 0; i <= meses; i++)
                    {
                        bool continuabarra = false;
                        try
                        {

                            bool exist = toReturn.Count > indexCompra + 1;

                            if (exist)
                                continuabarra = compra.FinLectura.AddDays(-1).Day > toReturn[indexCompra].InicioLectura.Day;


                        }
                        catch (Exception ex)
                        {

                            throw ex;
                        }
                        

                        var consumoMes = compra.getMonthAmount(fecha.Month, fecha.Year) * compra.FactorDeConversion;

                        var consumoMesRdn = Math.Round(consumoMes,2);

                        var cantidadMensual = new CantidadPorMesClass
                        {
                            Anio = fecha.Year,
                            Mes = fecha.Month,

                            ContinuaBarra = continuabarra,
                            DiaInicio = fecha.Day,
                            DiaFin = compra.FinLectura.Day,

                            CantidadEnkWh = (consumoMesRdn).ToString("#.##"),
                            Llenado = compra.getPercentageFill(fecha.Month, fecha.Year)
                        };

                        cantidadPorMes.Add(cantidadMensual);
                        fecha = fecha.AddMonths(1);
                    }
                    compra.CantidadPorMes = cantidadPorMes;
                    compra.DistintoAnio = compra.FinLectura.Year != compra.InicioLectura.Year;
                }

                indexCompra++;
            }

            var lv = 0;

            var comprasClasificadas = new List<CompraTablaEnergetico>();


            do
            {

                var comprasSinClasificar = new List<CompraTablaEnergetico>();

                for (int i = 0; i <= toReturn.Count() - 1; i++)
                {
                    var a = comprasClasificadas.Where(c => c.NumeroCliente == toReturn[i].NumeroCliente && c.AnioFechaFin == toReturn[i].AnioFechaFin && c.CompraSolapadaLv == lv + 1);

                    var existcompIni = a.Any((c => c.InicioLectura < toReturn[i].InicioLectura && toReturn[i].InicioLectura < c.FinLectura));

                    var existcompIFin = a.Any((c => c.InicioLectura < toReturn[i].FinLectura && toReturn[i].FinLectura < c.FinLectura));

                    var sameDate = a.Any((c => c.InicioLectura == toReturn[i].InicioLectura && toReturn[i].FinLectura == c.FinLectura));

                    var bigCompraIni = a.Any((c => c.InicioLectura > toReturn[i].InicioLectura && toReturn[i].FinLectura > c.InicioLectura));

                    var bigCompraFin = a.Any((c => c.FinLectura > toReturn[i].InicioLectura && toReturn[i].FinLectura > c.FinLectura));


                    var existCompra = existcompIni || existcompIFin || sameDate || bigCompraIni || bigCompraFin;

                    //var existCompra = compraQ.Where(c => c.AnioFechaFin == compra.AnioFechaInicio && c.FinLectura.Month == compra.InicioLectura.Month && c.Id != compra.Id && c.FinLectura > compra.InicioLectura).ToList();

                    if (existCompra)
                    {
                        comprasSinClasificar.Add(toReturn[i]);
                    }

                    if (!existCompra)
                    {
                        toReturn[i].CompraSolapadaLv = lv + 1;
                        comprasClasificadas.Add(toReturn[i]);
                    }

                }

                lv = lv + 1;

                toReturn = comprasSinClasificar;


            } while (toReturn.Count() != 0);


            return comprasClasificadas;
        }

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        public async Task<CompraParaValidarDetalleModel> GetParaValidar(long compraId)
        {

            var compra = await _repoCompra.Query().Where(c => c.Id == compraId)
                .Include(c => c.Energetico)
                .Include(c => c.NumeroCliente)
                .Include(m => m.CompraMedidor)
                .Include(u => u.UnidadMedida)
                .Include(c => c.EstadoValidacion)
                .FirstOrDefaultAsync();


            var compraParaRetornar = _mapper.Map<CompraParaValidarDetalleModel>(compra);

            if (compraParaRetornar != null) {

                var usuaModificador = await _userManager.FindByIdAsync(compraParaRetornar.RevisadoPor);
                compraParaRetornar.RevisadoPor = $"{usuaModificador?.Nombres} {usuaModificador?.Apellidos}";

                usuaModificador = await _userManager.FindByIdAsync(compraParaRetornar.ModifiedBy);
                compraParaRetornar.ModifiedBy = $"{usuaModificador?.Nombres} {usuaModificador?.Apellidos}";

                foreach (var itemConsumos in compraParaRetornar.ListaMedidores)
                {
                    var consumo = _compraMedidorRepo.Query().Where(cm => cm.Id == itemConsumos.Id)
                        .Include(cm => cm.Medidor)
                        .Include(cm => cm.ParametroMedicion)
                        .Include(cm => cm.UnidadMedida)
                        .FirstOrDefault();

                    itemConsumos.Medidor = consumo.Medidor.Numero;
                    itemConsumos.ParametroMedicion = consumo.ParametroMedicion?.Nombre;
                    itemConsumos.UnidadMedida = consumo.UnidadMedida?.Nombre;
                    itemConsumos.Abrv = consumo.UnidadMedida?.Abrv;

                    compraParaRetornar.TieneConsumos = true;
                }
            }

            return compraParaRetornar;

        }

        public async Task<string> AccionEstado(long compraId, string accion, string obs)
        {
            var compra = await _repoCompra.Query().Where(c => c.Id == compraId).FirstOrDefaultAsync();

            string estado = string.Empty;

            switch (accion.ToString().ToLower())
            {
                case "v":
                    estado = "ok";
                    break;
                case "o":
                    estado = "observado";
                    break;
                default:
                    //sin_revision
                    break;
            }

            compra.EstadoValidacionId = estado;
            compra.RevisadoPor = _currentUser.Id;
            compra.ReviewedAt = DateTime.Now;
            compra.ObservacionRevision = obs;

            _repoCompra.Update(compra);
           await _repoCompra.SaveAll();
            

            return $"{_currentUser.Nombres} {_currentUser.Apellidos}";
        }
    }
}
