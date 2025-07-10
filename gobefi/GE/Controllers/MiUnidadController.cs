using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
using System.Threading.Tasks;
using AutoMapper;
using DocumentFormat.OpenXml.Office2010.ExcelAc;
using GE.Core.Contracts.Services;
using GobEfi.Business.Validaciones;
using GobEfi.Web.Core;
using GobEfi.Web.Core.Attributes;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs;
using GobEfi.Web.Helper;
using GobEfi.Web.Migrations;
using GobEfi.Web.Models;
using GobEfi.Web.Models.DivisionModels;
using GobEfi.Web.Models.EmpresaDistribuidoraModels;
using GobEfi.Web.Models.EnergeticoDivisionModels;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Models.EnergeticoUnidadMedidaModels;
using GobEfi.Web.Models.MedidorDivisionModels;
using GobEfi.Web.Models.MedidorModels;
using GobEfi.Web.Models.NumeroClienteModels;
using GobEfi.Web.Models.TipoTarifaModels;
using GobEfi.Web.Models.TipoUsoModels;
using GobEfi.Web.Models.UnidadMedidaModels;
using GobEfi.Web.Services.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MoreLinq;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;

namespace GobEfi.Web.Controllers
{
    
    public class MiUnidadController : BaseController
    {
        private readonly IDivisionService divisionService;
        private readonly IEdificioService edificioService;
        private readonly IEnergeticoService energeticoService;
        private readonly IEnergeticoDivisionService energeticoDivisionService;
        private readonly INumeroClienteService numClienteService;
        private readonly IEmpresaDistribuidoraService empresaDistribuidoraService;
        private readonly ITipoTarifaService tipoTarifaService;
        private readonly IMedidorService medidorService;
        private readonly IEnergeticoUnidadMedidaService energeticoUnidadMedidaService;
        private readonly IUnidadMedidaService unidadMedidaService;
        private readonly IMedidorDivisionService _servMedidorDivision;
        private readonly IMenuService _servMenu;
        private readonly ILogger logger;
        private readonly IMapper mapper;
        private readonly IUsuarioService _servUsuario;
        private readonly IServicioService _servicioService;
        private readonly IMiUnidadService _miUnidadService;
        private readonly IConfiguration _configuration;
        private readonly Usuario _currentUser;

        public MiUnidadController(
            ApplicationDbContext context,
            IDivisionService divisionService,
            IEdificioService edificioService,
            IEnergeticoService energeticoService,
            IEnergeticoDivisionService energeticoDivisionService,
            INumeroClienteService numClienteService,
            IEmpresaDistribuidoraService empresaDistribuidoraService,
            ITipoTarifaService tipoTarifaService,
            IMedidorService medidorService,
            IEnergeticoUnidadMedidaService energeticoUnidadMedidaService,
            IUnidadMedidaService unidadMedidaService,
            IMedidorDivisionService servMedidorDivision,
            IMenuService servMenu,
            ILoggerFactory loggerFactory,
            IMapper mapper,
            UserManager<Usuario> manager,
            IUsuarioService servUsuario,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService,
            IServicioService servicioService,
            IMiUnidadService miUnidadService,
            IConfiguration configuration
            ) : base(context, manager, httpContextAccessor, usuarioService)
        {
            this.logger = loggerFactory.CreateLogger<MiUnidadController>();

            this.divisionService = divisionService;
            this.edificioService = edificioService;
            this.energeticoService = energeticoService;
            this.energeticoDivisionService = energeticoDivisionService;
            this.numClienteService = numClienteService;
            this.empresaDistribuidoraService = empresaDistribuidoraService;
            this.tipoTarifaService = tipoTarifaService;
            this.medidorService = medidorService;
            this.energeticoUnidadMedidaService = energeticoUnidadMedidaService;
            this.unidadMedidaService = unidadMedidaService;
            _servMedidorDivision = servMedidorDivision;
            _servMenu = servMenu;
            this.mapper = mapper;
            _servUsuario = servUsuario;
            _servicioService = servicioService;
            _miUnidadService = miUnidadService;
            _configuration = configuration;
            _currentUser = (manager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;

            //HttpContext.Session.SetString("UserId", usuario.Id);
        }

        #region[LISTAR]
        [SetReturnUrl]
        public async Task<IActionResult> Index(DivisionRequest request)
        {
            

            if (await _servicioService.IsGEV3(_currentUser))
            {
                return RedirectToAction("Index", "MiUnidadV2");
            }

            ViewBag.sexoId = usuario.SexoId == null ? 0 : usuario.SexoId;
            return View();
        }
        #endregion

        #region[VER]
        public async Task<IActionResult> Ver(long id)
        {
            // logger.LogInformation("Mi Unidad Ver");

            string rutaActual = httpContextAccessor.HttpContext.Request.Path.Value;

            //ViewData["subMenus"] = await _servMenu.GetSubMenusByMenuAndRoles(rutaActual, this.usuario.Id);

            ViewData["subMenus"] = await _miUnidadService.GetSubMenusByMenuAndRolesIncludeMi(rutaActual, this.usuario.Id,id);

            HttpContext.Session.SetString("DivisionId", id.ToString());


            var model = divisionService.Ver(id);
            //var model = new DivisionVerModel();
            if (model == null)
            {
                return RedirectToAction("Index");
            }
           
            

            return View(model);
        }

        private async Task<string> GetPatentes(long servicioId, string vids) 
        {
            if (string.IsNullOrEmpty(vids))
            {
                return "";
            }
            var arrVids = vids.Split(',');

            if(arrVids.Length == 0)
            {
                return "";
            }
            
            var url = _configuration.GetValue<string>("ApiConfiguration:apiFlota");
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{url}/api/vehiculos/byServicioId/{servicioId}");
            var list = new List<VehiculoDTO>();
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<List<VehiculoDTO>>();

            }

            var patentes = new List<string>();

            foreach (var id in arrVids)
            {
                var vehiculo = list.Where(x => x.Id.ToString() == id).FirstOrDefault();
                if (vehiculo != null)
                {
                    patentes.Add(vehiculo.Patente);
                }
            }

            return string.Join(",", patentes);
        }
        #endregion

        public async Task<IActionResult> Divisiones(DivisionRequest request)
        {
            var indexModel = await divisionService.AllByUser(usuario.Id);

            //return PartialView(@"Shared/_DivisionesTable", indexModel);
            return Ok(indexModel);
        }

        [CheckSessionOut]
        public async Task<IActionResult> InformacionGeneral(long id)
        {
            if (id == 0)
            {
                return BadRequest($"Error en el identificador de la unidad.");
            }

            // logger.LogInformation("Mi Unidad Información general");

            ViewData["permisos"] = GetPermisions();

            ViewData["DivisionId"] = id;

            //var url = _configuration.GetValue<string>("ApiConfiguration:apiFlota");
            //var servicioId = await context.Divisiones.Where(x => x.Id == id).Select(x => x.Servicio.Id).FirstOrDefaultAsync();
            //var client = new HttpClient();
            //HttpResponseMessage response = await client.GetAsync($"{url}/api/vehiculos/byServicioId/{servicioId}");
            //var list = new List<VehiculoDTO>();
            //if (response.IsSuccessStatusCode)
            //{
            //    list = await response.Content.ReadAsAsync<List<VehiculoDTO>>();

            //}

            //var options = new SelectList(list, "Id", "Patente");
            //ViewData["Patentes"] = options;
            var model = divisionService.Ver(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            

            return View(model);
        }

        public IActionResult InformacionGeneralViewEdificio(long id)
        {
            return View();
        }

        public async Task<IActionResult> InformacionGeneralView(long id)
        {
            
            var model = divisionService.Ver(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            model.Patentes = await GetPatentes(model.ServicioId, model.VehiculosIds);

            ViewData["permisos"] = GetPermisions("/miunidad/informaciongeneral/");
            ViewData["isAdmin"] = this._isAdmin;


            return PartialView(@"Shared/_ViewDivision", model);
        }

        [HttpGet]
        [CheckSessionOut]
        public async Task<IActionResult> InformacionGeneralEdit(long id)
        {
            var model = divisionService.Ver(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.AniosDeConstruccion = Anios();
            return PartialView(@"Shared/_EditDivision", model);
        }
        
        [HttpPost]
        public async Task<IActionResult> InformacionGeneralSave(DivisionEditInfGeneralModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var division = divisionService.Get(model.Id);

            model.EdificioId = division.EdificioId;
            model.ServicioId = division.ServicioId;
            model.Version = ++division.Version;

            //tipo agrupamiento y tipo unidad, se sacan ya que causan conflicto de FK 27/03/2019
            //model.TipoAgrupamientoId = division.TipoAgrupamientoId;
            //model.TipoUnidadId = division.TipoUnidadId;

            model.Nombre = model.Nombre;
            model.ModifiedBy = usuario.Id;
            

            divisionService.Update(model);

            var divisionParaRetornar = await divisionService.GetAsync(model.Id);

            return Json(new {
                success = true,
                responseText = "Your message successfuly sent!",
                data = divisionParaRetornar //model
            });
        }

        public IActionResult MaquinasEquipos(long id)
        {
            return View("MaquinasEquipos");
        }


        #region Pagina MiUnidad/Energeticos
        [CheckSessionOut]
        [SetReturnUrl]
        public IActionResult Energeticos(EnergeticoRequest request)
        {
            ViewData["permisos"] = GetPermisions();

            var indexModel = new EnergeticosIndexModel
            {
                Items = energeticoService.All()
            };

            List<EnergeticoDivision> energeticosActivos = energeticoDivisionService.GetByDivisionId(long.Parse(HttpContext.Session.GetString("DivisionId"))).ToList();

            foreach (var item in indexModel.Items)
            {
                EnergeticoDivision enerDiv = energeticosActivos.Where(a => a.EnergeticoId == item.Id && a.Active && a.NumeroClienteId == null).FirstOrDefault();

                item.Activo = (enerDiv != null);
            }

            return View(indexModel);
        }

        [HttpPost]
        [CheckSessionOut]
        public IActionResult CheckUnidadMedida(long enerId)
        {
            long divisionId = long.Parse(HttpContext.Session.GetString("DivisionId"));

            //verificar si el energetico tiene mas unidades de medida
            //si el energetico tiene solo 1 unidad de medida se retorna "ok"
            if (!energeticoUnidadMedidaService.hasMoreOne(enerId))
            {
                EnergeticoUnidadMedidaModel energeticoUnidadMedida = energeticoUnidadMedidaService.getByEnergeticoId(enerId).FirstOrDefault();

                return Json(new { success = true, responseText = "NO", data = energeticoUnidadMedida?.UnidadMedidaId });
            }
                

            //si el energetico tiene >= 1 unidad de medida, se retorna el listado
            List<EnergeticoUnidadMedidaModel> unidadMedidaIds = energeticoUnidadMedidaService.getByEnergeticoId(enerId);
            List<UnidadMedidaModel> unidadMedidaToView = unidadMedidaService.Get(unidadMedidaIds);

            return Json(new { success = true, responseText = "SI", data = unidadMedidaToView });
        }

        [HttpPost]
        public IActionResult SwitchEnergetico([FromBody] EnergeticoSwitchModel cambioEstado)
        {
            // long divisionId = long.Parse(HttpContext.Session.GetString("DivisionId"));
            long divisionId = cambioEstado.DivisionId;
            long energeticoId = cambioEstado.EnergeticoId;


            //Activar o Desactivar un Energetico de la Division seleccionada
            if (cambioEstado.State)
            {
                EnergeticoDivisionModel enerDiv = new EnergeticoDivisionModel
                {
                    EnergeticoId = energeticoId,
                    DivisionId = divisionId//,
                    //UnidadMedidaId = cambioEstado.UnidadMedidaId
                };

                this.energeticoDivisionService.Insert(enerDiv);
            }
            else
            {
                var switcher = this.energeticoDivisionService.Get(divisionId, energeticoId).FirstOrDefault(a => a.NumeroClienteId == null);
                
                this.energeticoDivisionService.Update(switcher);

            }

            return Json(new { success = true, responseText = "Your message successfuly sent!" });
        }

        [HttpPost]
        [CheckSessionOut]
        public async Task<IActionResult> SwitchMedidorDivision([FromBody] MedidorDivisionSwitchModel asociarMedidor)
        {
            asociarMedidor.DivisionId = long.Parse(HttpContext.Session.GetString("DivisionId"));

            if (asociarMedidor.Activo)
            {
                _servMedidorDivision.Insert(asociarMedidor);
            }
            else
            {
                _servMedidorDivision.Update(asociarMedidor);
            }

            return Ok(asociarMedidor);

        }
        #endregion

        #region Pagina MiUnidad/EnergeticosConfig
        public IActionResult EnergeticosConfig(long id)
        {
            HttpContext.Session.SetString("EnergeticoId", id.ToString());

            //var usuarioActual = (manager.GetUserAsync(httpContextAccessor.HttpContext.User)).Result;
            // se entrega rutal /miunidad/Energeticos/ ya que EnergeticosConfig dependen de esta url
            //ViewData["permisos"] = _servUsuario.TienePermisos(usuarioActual, "/miunidad/Energeticos/");
            ViewData["permisos"] = GetPermisions("/miunidad/Energeticos/");
            ViewData["energeticoId"] = id;
            getDataEnergeticosConfig();

            return View(new NumeroClienteModel());
        }


        /// <summary>
        /// Obtiene y carga los datos en sessiones necesarios para cargar la pagina MiUnidad/EnergeticosConfig
        /// </summary>
        [CheckSessionOut]
        private void getDataEnergeticosConfig()
        {

            long divisionId = long.Parse(HttpContext.Session.GetString("DivisionId"));
            long energeticoId = long.Parse(HttpContext.Session.GetString("EnergeticoId"));

            EnergeticoModel ener = energeticoService.Get(energeticoId);

            List<NumeroClienteModel> clientToView = numClienteService.Get(divisionId, energeticoId).ToList();

            long comunaId = this.edificioService.GetComunaByDivision(divisionId);

            EnergeticoConfigModel itemView = new EnergeticoConfigModel
            {
                Icono = ener.Icono,
                Nombre = ener.Nombre,
                Clientes = clientToView,
                Medidores = GetMedidores(),
                EmpresaDistribuidora = this.empresaDistribuidoraService.GetByEnergeticoComuna(energeticoId, comunaId).ToList(),
                TipoTarifa = tipoTarifaService.All().ToList()
            };


            ViewData["EnergeticoConfigModelData"] = itemView;
            ViewBag.PermitePotenciaSum = ener.PermitePotenciaSuministrada;
            ViewBag.PermiteTipoTarifa = ener.PermiteTipoTarifa;
            ViewBag.Mensaje = "";

        }

        [CheckSessionOut]
        [HttpPost]
        public IActionResult SaveMedidor(MedidorModel model)
        {

            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var divisionId = HttpContext.Session.GetString("DivisionId")._toLong();

                if (model.DivisionId == 0)
                {
                    model.DivisionId = divisionId;
                }


                long newId = 0;

                if (model.Id == 0)
                {
                    newId = medidorService.Insert(model);
                    //newId = medidorService.InsertaAsocia(model, divisionId);
                    _servMedidorDivision.Insert(new MedidorDivisionSwitchModel { MedidorId = newId, DivisionId = divisionId, Activo = true });
                }
                else
                    medidorService.Update(model);

                getDataEnergeticosConfig();

                var listMedidores = medidorService.GetByNumClienteId(model.NumeroClienteId);

                ICollection<MedidorParaAsociar> medidores = ParseMedidor(listMedidores, divisionId);

                return PartialView(@"Shared/_AsociarMedidores", medidores);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }           
        }

        private ICollection<MedidorParaAsociar> ParseMedidor(List<MedidorModel> origen, long divisionId)
        {
            ICollection<MedidorParaAsociar> medidores = new List<MedidorParaAsociar>();

            foreach (var medidorItem in origen)
            {
                MedidorParaAsociar paraAsociar = new MedidorParaAsociar
                {
                    Id = medidorItem.Id,
                    NumeroMedidor = medidorItem.Numero,
                    TipoMedidor = "¿donde lo obtengo?",
                    Asociado = medidorService.EstanAsociado(medidorItem.Id, divisionId)
                };

                medidores.Add(paraAsociar);
            }

            return medidores;
        }

        [CheckSessionOut]
        private List<MedidorModel> GetMedidores()
        {
            long divisionId = long.Parse(HttpContext.Session.GetString("DivisionId"));
            long energeticoId = long.Parse(HttpContext.Session.GetString("EnergeticoId"));

            var medidores = medidorService.GetByDivisionId(divisionId);

            var numClientes = medidores.Select(m => m.NumeroClienteId).Distinct().ToList();

            foreach (var numClienteItem in numClientes)
            {
                bool existeRelacion = energeticoDivisionService.ExisteRelacion(divisionId, energeticoId, numClienteItem);

                if (!existeRelacion)
                {
                    var medidoresAQuitar = medidores.Where(m => m.NumeroClienteId == numClienteItem).ToList();

                    foreach (var medidorAQuitar in medidoresAQuitar)
                    {
                        medidores.Remove(medidorAQuitar);
                    }
                }

            }

            return medidores;

            //long energeticoId = long.Parse(HttpContext.Session.GetString("EnergeticoId"));

            //EnergeticoModel ener = energeticoService.Get(energeticoId);

            //List<NumeroClienteModel> clientToView = numClienteService.Get(divisionId, energeticoId).ToList();

            //List<long> IdsFromNumCliente = new List<long>();

            //for (int i = 0; i < clientToView.Count; i++)
            //    IdsFromNumCliente.Add(clientToView[i].Id);

            //return medidorService.GetByDivisionId(IdsFromNumCliente);
        }

        /// <summary>
        /// Listar en base a los Numeros de Clientes desplegados
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult ListMedidores()
        {
            getDataEnergeticosConfig();


            ICollection<MedidorModel> medidores = ((EnergeticoConfigModel)ViewData["EnergeticoConfigModelData"]).Medidores;

            return Json(medidores);
            
        }

        [CheckSessionOut]
        [HttpPost]
        public IActionResult SaveCliente(NumeroClienteModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!string.IsNullOrEmpty(model.Potencia)) {
                model.PotenciaSuministrada = Convert.ToDecimal(model.Potencia.Replace('.', ','));
            }

            

            //ViewBag.Mensaje = "";

            List<NumeroClienteModel> listNumClientes = new List<NumeroClienteModel>();
            IEnumerable<NumeroClienteModel> numClientes;

            long divisionId = long.Parse(HttpContext.Session.GetString("DivisionId"));
            long energeticoId = long.Parse(HttpContext.Session.GetString("EnergeticoId"));
            ViewData["permisos"] = GetPermisions("/miunidad/Energeticos/");

            var checkNumCliente = NClienteAutoComplete(new NumeroClienteAutoComplete { EmpresaDistribuidoraId = model.EmpresaDistribuidoraId, Numero = model.Numero }).Result;
            var checkNumClienteJson = ((JsonResult)checkNumCliente).Value;
            var checkNumClienteObject = JsonConvert.DeserializeObject<NumeroClienteAutoCompleteResult>(JsonConvert.SerializeObject(checkNumClienteJson));

            bool existeRelacion = energeticoDivisionService.ExisteRelacion(divisionId, energeticoId, model.Id);

            if (checkNumClienteObject.Success && !existeRelacion)
            {
                var medidoresDelNumCliente = AsociarExistente(checkNumClienteObject.Data);

                var medidorObject = new MedidorModel { NumeroClienteId = checkNumClienteObject.Data.Id, Medidores = medidoresDelNumCliente };


                listNumClientes.Add(checkNumClienteObject.Data);
                numClientes = listNumClientes;

                ViewBag.NumCliente = SelectHelper.LlenarDDL(numClientes);

                return PartialView(@"Shared/_ModalMedidor", medidorObject);
            }


            ICollection<MedidorParaAsociar> medidoresParaMostrar = new List<MedidorParaAsociar>();

            if (model.Id == 0)
            {
                // es un nuevo cliente por lo cual no tiene ningun medidor agregado
                model.Id = numClienteService.Insert(model);

                if (model.Id == -1)
                    return Json(new { success = false, responseText = "Numero de cliente ya existe a la empresa distribuidora", data = model.Id });


                var unidadMedida = this.energeticoDivisionService.Get(divisionId, energeticoId).FirstOrDefault(a => a.NumeroClienteId == null).UnidadMedidaId;

                EnergeticoDivisionModel EnergeticoDivision = new EnergeticoDivisionModel
                {
                    NumeroClienteId = model.Id,
                    EnergeticoId = energeticoId,
                    DivisionId = divisionId,
                    UnidadMedidaId = unidadMedida
                };

                energeticoDivisionService.Insert(EnergeticoDivision);
            }
            else
            {
                // es un numero de cliente ya registrado por lo cual puede tener algun medidor ingresado que hay que mostrar 
                numClienteService.Update(model);

                var medidores = medidorService.GetByNumClienteId(model.Id);
                medidoresParaMostrar = ParseMedidor(medidores, divisionId);  
            }
                


            //List<NumeroClienteModel> listNumClientes = new List<NumeroClienteModel>();
            listNumClientes.Add(model);
            numClientes = listNumClientes;

            ViewBag.NumCliente = SelectHelper.LlenarDDL(numClientes);

            return PartialView(@"Shared/_ModalMedidor", new MedidorModel { NumeroClienteId = model.Id, Medidores = medidoresParaMostrar });

            //return Json(new { success = true, responseText = "Cliente guardado con exito.", data = model.Id});
        }

        [CheckSessionOut]
        [HttpPost]
        public ICollection<MedidorParaAsociar> AsociarExistente(NumeroClienteModel model)
        {
            long divisionId = long.Parse(HttpContext.Session.GetString("DivisionId"));
            long energeticoId = long.Parse(HttpContext.Session.GetString("EnergeticoId"));

            //var medidores = medidorService.GetByNumClienteId(model.Id);

            //foreach (var item in medidores)
            //{
            //    item._compartido = true;

            //    medidorService.Update(item);
            //}

            var EnergeticoDivision = this.energeticoDivisionService.Get(divisionId, energeticoId).FirstOrDefault();

            EnergeticoDivisionModel entityToAsociate = new EnergeticoDivisionModel
            {
                NumeroClienteId = model.Id,
                EnergeticoId = energeticoId,
                DivisionId = divisionId,
                UnidadMedidaId = EnergeticoDivision.UnidadMedidaId
            };

            energeticoDivisionService.Insert(entityToAsociate);


            var medidores = medidorService.GetByNumClienteId(model.Id);

            var medidoresParaRetorno = ParseMedidor(medidores, divisionId);

            return medidoresParaRetorno;

            //return Json(new { success = true, responseText = "Asociacion realizada con exito.", data = model.Id });
        }


        [CheckSessionOut]
        [HttpPost]
        public IActionResult ListClientes()
        {
            long divisionId = long.Parse(HttpContext.Session.GetString("DivisionId"));
            long energeticoId = long.Parse(HttpContext.Session.GetString("EnergeticoId"));

            ICollection<NumeroClienteModel> cltes = numClienteService.Get(divisionId, energeticoId).ToList();

            return Json(cltes);
        }


        /// <summary>
        /// Metodo que valida si el numero de cliente existe en la base de datos
        /// valida si el numero de cliente se creo con el energetico actual 
        /// </summary>
        /// <param name="numCliente">Numero de cliente tipeado en la caja de texto</param>
        /// <returns>Mensaje </returns>
        [HttpPost]
        [CheckSessionOut]
        public async Task<IActionResult> NClienteAutoComplete([FromBody] NumeroClienteAutoComplete numCliente)
        {
            long energeticoIdActual = long.Parse(HttpContext.Session.GetString("EnergeticoId"));


            var numClienteFromService = await numClienteService.GetNumeroClientesByNum(numCliente.Numero, numCliente.EmpresaDistribuidoraId);

            if (numClienteFromService == null)
            {
                //return NotFound(numClienteFromService);
                return Json(new
                {
                    success = false
                });
            }

            var energeticoModel = energeticoService.CheckFirstEnergetico(numClienteFromService.Id, energeticoIdActual);

            if (energeticoModel != null && energeticoModel.Id != energeticoIdActual)
            {
                return Json(new
                {
                    success = true,
                    text = $"El numero de cliente <strong>{numClienteFromService.Numero}</strong> se encuentra asociado al energetico <strong>{energeticoModel.Nombre}</strong>. ¿Desea asociar de todos modos?",
                    data = numClienteFromService
                });
            }
            
            return Json(new
            {
                success = true,
                text = $"El numero de cliente <strong>{numClienteFromService.Numero}</strong> ya existe. ¿Desea asociar alguno de estos medidores a tu Unidad?",
                data = numClienteFromService
            });
        }

        public ActionResult GetNCliente(string id)
        {
            NumeroClienteModel numCliente = numClienteService.Get(long.Parse(id));
            return Json(numCliente);
        }

        public ActionResult GetMedidor(long id)
        {
            MedidorModel medidor = medidorService.Get(id);
            return Json(medidor);
        }

        [CheckSessionOut]
        public ActionResult DeleteNCliente(long id)
        {
            long divisionId = long.Parse(HttpContext.Session.GetString("DivisionId"));
            long energeticoId = long.Parse(HttpContext.Session.GetString("EnergeticoId"));

            energeticoDivisionService.Delete(id, divisionId, energeticoId);

            //numClienteService.Delete(id);

            medidorService.DesasociarbyNumCliente(id, divisionId);
            
            //medidorService.DeleteByNumClienteId(id);

            return Json(new { success = true, responseText = "Cliente Eliminado con exito.", data = id });
        }


        /// <summary>
        /// Elimina la relacion Medidor-Unidad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [CheckSessionOut]
        public ActionResult DeleteMedidor(long id)
        {
            MedidorModel medidor = medidorService.Get(id);
            if (medidor == null)
                BadRequest("Medidor no encontrado");

            long divisionId = long.Parse(HttpContext.Session.GetString("DivisionId"));

            _servMedidorDivision.Delete(id, divisionId);
            //medidorService.Delete(id);
            


            return Json(new { success = true, responseText = "Medidor Eliminado con exito.", data = id });
        }
        

        #endregion


        public IActionResult DisenioPasivo(long id)
        {
            return View("DisenioPasivo");
        }
        
        private SortModel SortItems()
        {
            var order = new SortModel();
            order.AddSortField("Id", "id");
            order.AddSortField("Nombre División", "nombre");
            order.AddSortField("Año Construcción", "anyo");
            return order;
        }

        public IActionResult ConsumosInteligentes()
        {
            return View();
        }

        public async Task<IActionResult> ActualizacionUnidad(long id)
        {
            return View();
        }

        public async Task<IActionResult> Sistemas(long id)
        {
            ViewData["permisos"] = GetPermisions();
            return View();
        }


    }
}