using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Core.Contracts.Services;
using Microsoft.Extensions.Logging;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using GobEfi.Web.Models.DivisionModels;
using GobEfi.Web.Services.Request;
using Microsoft.AspNetCore.Identity;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Helper;
using System;
using System.Collections.Generic;
using GobEfi.Business.Validaciones;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using GobEfi.Web.DTOs;

namespace GobEfi.Web.Controllers
{
    public class AdminDivisionController : BaseController
    {
        private readonly IDivisionService _servDivision;
        private readonly ITipoUsoService _servTipoUso;
        private readonly ITipoPropiedadService _servTipoPropiedad;
        private readonly IRegionService _servRegion;
        private readonly IProvinciaService _servProvincia;
        private readonly IComunaService _servComuna;
        private readonly IEdificioService _servEdificio;
        private readonly IInstitucionService _servInstitucion;
        private readonly IServicioService _servServicio;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;

        [TempData]
        public string StatusMessage { get; set; }

        public AdminDivisionController(
            ApplicationDbContext context,
            IDivisionService servDivision,
            ITipoUsoService servTipoUso,
            ITipoPropiedadService servTipoPropiedad,
            IRegionService servRegion,
            IProvinciaService servProvincia,
            IComunaService servComuna,
            IEdificioService servEdificio,
            IInstitucionService servInstitucion,
            IServicioService servServicio,
            IMapper mapper,
            ILoggerFactory factory,
            IHttpContextAccessor httpContextAccessor,
            UserManager<Usuario> manager,
            IConfiguration configuration,
            IUsuarioService usuarioService) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _servDivision = servDivision;
            _servTipoUso = servTipoUso;
            _servTipoPropiedad = servTipoPropiedad;
            _servRegion = servRegion;
            _servProvincia = servProvincia;
            _servComuna = servComuna;
            _servEdificio = servEdificio;
            _servInstitucion = servInstitucion;
            _servServicio = servServicio;
            _mapper = mapper;
            _configuration = configuration;
            _logger = factory.CreateLogger<AdminDivisionController>();
        }

        private void LlenarCombosIndex()
        {
            //ViewBag.Institucion = SelectHelper.LlenarDDL(_servInstitucion.GetInstituciones().Result, true);
            ViewBag.Institucion = SelectHelper.LlenarDDL(_servInstitucion.GetAsociados().Result, true);
        }

        // GET: AdminDivision
        public async Task<IActionResult> Index(DivisionRequest request)
        {
            ViewData["permisos"] = this.permisos;
            ViewData["ajustes"] = this.ajustes;
            ViewData["userServiceIsPmg"] = this._userServiceIsPmg;

            var indexModel = new DivisionIndexModel
            {
                Items = await _servDivision.PagedAsync(request),
                Sort = SortItems()
            };

            LlenarCombosIndex();
            ViewBag.ServicioId = request.ServicioId;

            return View(indexModel);
        }

        // GET: AdminDivision/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var division = await context.Divisiones
                .Include(d => d.Edificio)
                .Include(d => d.Servicio)
                .Include(d => d.TipoPropiedad)
                .Include(d => d.TipoUnidad)
                .Include(d => d.TipoUso)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (division == null)
            {
                return NotFound();
            }

            division.ModifiedBy = NombreCompletoUsuario(division.ModifiedBy);
            division.CreatedBy = NombreCompletoUsuario(division.CreatedBy);

            var data = _mapper.Map<DivisionDetailsModel>(division);
            ObtenerDataForDetails();

            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsEdit(long id,
            [Bind("Id,NroRol,SinRol,JustificaRol")] DivisionDetailsModel division)
        {
            if (id != division.Id)
            {
                return NotFound();
            }

            Validaciones("1",1,null,null,null,division.SinRol,division.JustificaRol,division.NroRol);

            if (!ModelState.IsValid)
            {

                var divisionModel = await context.Divisiones
               .Include(d => d.Edificio)
               .Include(d => d.Servicio)
               .Include(d => d.TipoPropiedad)
               .Include(d => d.TipoUnidad)
               .Include(d => d.TipoUso)
               .FirstOrDefaultAsync(m => m.Id == id);
                if (division == null)
                {
                    return NotFound();
                }

                divisionModel.ModifiedBy = NombreCompletoUsuario(divisionModel.ModifiedBy);
                divisionModel.CreatedBy = NombreCompletoUsuario(divisionModel.CreatedBy);

                var data = _mapper.Map<DivisionDetailsModel>(divisionModel);
                data.SinRol = division.SinRol;
                data.NroRol = division.NroRol;
                data.JustificaRol = division.JustificaRol;
               
                ObtenerDataForDetails();

                return View("Details", data);
            }


            try
            {
                _servDivision.DetailsUpdate(division);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DivisionExists(division.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));

        }




        private void ObtenerDataForDetails()
        {
            //ViewBag.Regiones = SelectHelper.LlenarDDL(_servRegion.All(), true);
            //ViewBag.Instituciones = SelectHelper.LlenarDDL(_servInstitucion.GetAsociados().Result, true);
            ViewBag.TiposDeUso = SelectHelper.LlenarDDL(_servTipoUso.All(), true);
            ViewBag.TiposDePropiedad = SelectHelper.LlenarDDL(_servTipoPropiedad.All(), true);


            ViewBag.AniosDeConstruccion = Anios();
        }

        private void ObtenerData()
        {
            ViewBag.Regiones = SelectHelper.LlenarDDL(_servRegion.All(), true);
            ViewBag.Instituciones = SelectHelper.LlenarDDL(_servInstitucion.GetAsociados().Result, true);
            ViewBag.InstitucionesResponsable = SelectHelper.LlenarDDL(_servInstitucion.All());
            ViewBag.TiposDeUso = SelectHelper.LlenarDDL(_servTipoUso.All(), true);
            ViewBag.TiposDePropiedad = SelectHelper.LlenarDDL(_servTipoPropiedad.All(), true);


            ViewBag.AniosDeConstruccion = Anios();
        }

        private void ObtenerDataForEdit()
        {
            ViewBag.Provincias = SelectHelper.LlenarDDL(_servProvincia.All(), true);
            ViewBag.Comunas = SelectHelper.LlenarDDL(_servComuna.All(), true);
            ViewBag.Edificios = SelectHelper.LlenarDDL(_servEdificio.GetActivosByUser(), true);
            ViewBag.Servicios = SelectHelper.LlenarDDL(_servServicio.All(), true);
        }

        // GET: AdminDivision/Create
        public IActionResult Create()
        {
            if (!permisoAccion.Escritura)
            {
                return Unauthorized();
            }

            ObtenerData();
            ViewBag.Mensaje = StatusMessage;
            return View();
        }

        // POST: AdminDivision/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DivisionCreateModel division)
        {
            Validaciones(division.Superficie, division.NroFuncionarios,division.AccesoFactura,division.ServicioResponsableId,division.OrganizacionResponsable,division.SinRol,division.JustificaRol,division.NroRol);

            ModelState.Remove("AccesoFactura");
            ModelState.Remove("InstitucionResponsableId");
            ModelState.Remove("ServicioResponsableId");


            if (!ModelState.IsValid)
            {
                ObtenerData();

                return View(division);
            }

            var result = await _servDivision.InsertAsync(division);


            StatusMessage = result > 0 ? "Unidad fue creada satisfactoriamente" : "Error al crear unidad";

            return RedirectToAction(nameof(Create));
        }

        public async Task<JsonResult> GetVehiculosJson(long servicioId)
        {
            var url = _configuration.GetValue<string>("ApiConfiguration:apiFlota");
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{url}/api/vehiculos/byServicioId/{servicioId}");
            var list = new List<VehiculoDTO>();
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<List<VehiculoDTO>>();

            }

            return Json(list);
        }

        // GET: AdminDivision/Edit/5
        public async Task<IActionResult> Edit(long id)
        {

            if (!permisoAccion.Escritura)
            {
                return Unauthorized();
            }

            if (id == 0)
            {
                return NotFound();
            }

            var url = _configuration.GetValue<string>("ApiConfiguration:apiFlota");
            var servicioId = await context.Divisiones.Where(x => x.Id == id).Select(x => x.Servicio.Id).FirstOrDefaultAsync();
            var client = new HttpClient();
            HttpResponseMessage response = await client.GetAsync($"{url}/api/vehiculos/byServicioId/{servicioId}");
            var list = new List<VehiculoDTO>();
            if (response.IsSuccessStatusCode)
            {
                list = await response.Content.ReadAsAsync<List<VehiculoDTO>>();

            }

            var options = new SelectList(list, "Id", "Patente");
            ViewData["Patentes"] = options;

            //var division = await context.Divisiones.FindAsync(id);
            var division = _servDivision.GetForEdit(id);

            if (division == null)
            {
                return NotFound();
            }
            ObtenerData();
            ObtenerDataForEdit();

            return View(division);
        }

        private void Validaciones(string superficie, int nroFuncionarios,int? accesoFactura = null, int? ServicioResponsableId = null,string OrganizacionResponsable = null,bool SinRol = false, string JustificaRol=null, string NroRol = null)
        {
            if (superficie._toLong() <= 0)
            {
                ModelState.AddModelError("Superficie", "La superficie debe ser mayor a 0.");
            }

            //if (nroRol._toLong() < 0 || nroRol._toLong() > int.MaxValue)
            //{
            //    ModelState.AddModelError("NroRol", $"El Número de rol debe estar entre 1 y {int.MaxValue}.");
            //}

            if (nroFuncionarios <= 0)
            {
                ModelState.AddModelError("NroFuncionarios", "Número de funcionarios debe ser mayor a 0.");
            }

            if (accesoFactura == 0 )
            {
                ModelState.AddModelError("AccesoFactura", "Debe seleccionar una opción");
            }
            if (accesoFactura == 2 && ServicioResponsableId == null)
            {
                ModelState.AddModelError("ServicioResponsableId", "Debe seleccionar un Servicio responsable");
            }
            if (accesoFactura == 3 && OrganizacionResponsable == null)
            {
                ModelState.AddModelError("OrganizacionResponsable", "Debe ingresar una organizacion responsable");
            }
            if (SinRol && JustificaRol == null) {
                ModelState.AddModelError("JustificaRol", "Debe ingresar una justificación");
            }
            if (!SinRol && NroRol == null)
            {
                ModelState.AddModelError("NroRol", "Debe ingresar una Rol");
            }
            
        }

        // POST: AdminDivision/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, DivisionEditModel division)
        {
            if (id != division.Id)
            {
                return NotFound();
            }

            Validaciones(division.Superficie, division.NroFuncionarios, division.AccesoFactura, division.ServicioResponsableId, division.OrganizacionResponsable,division.SinRol,division.JustificaRol, division.NroRol);

            if (!ModelState.IsValid)
            {
                ObtenerData();
                ObtenerDataForEdit();

                return View(division);
            }

       
            try
            {
                await _servDivision.Update(division); // Añadido await
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DivisionExists(division.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
            
        }

        // GET: AdminDivision/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            DivisionDeleteModel divisionToView = _servDivision.GetForDelete(id);

            return View(divisionToView);
        }

        // POST: AdminDivision/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            _servDivision.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool DivisionExists(long id)
        {
            return context.Divisiones.Any(e => e.Id == id);
        }
    }
}
