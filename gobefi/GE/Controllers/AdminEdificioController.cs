using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Models.EdificioModels;
using GobEfi.Web.Core.Contracts.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;
using GobEfi.Web.Services.Request;
using GobEfi.Web.Models;
using GobEfi.Web.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Helper;

namespace GobEfiWeb.Controllers
{
    public class AdminEdificioController : BaseController
    {
        private readonly ITipoEdificioService _servTipoEdificio;
        private readonly IComunaService _servComuna;
        private readonly IEdificioService _servEdificio;
        private readonly IRegionService _servRegion;
        private readonly IMapper _mapper;
        private readonly IProvinciaService _servProvincia;
        private readonly ITipoUsoService _servTipoUso;
        private readonly ILogger _logger;

        //[TempData]
        //public string StatusMessage { get; set; }

        public AdminEdificioController(
            ApplicationDbContext context,
            ITipoEdificioService servTipoEdificio,
            IComunaService servComuna,
            IEdificioService servEdificio,
            IRegionService servRegion,
            ILoggerFactory factory,
            IMapper mapper, 
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IUsuarioService usuarioService,
            IProvinciaService servProvincia,
            ITipoUsoService servTipoUso
            ) : base(context, manager, httpContextAccessor, usuarioService)
        {
            _servTipoEdificio = servTipoEdificio;
            _servComuna = servComuna;
            _servEdificio = servEdificio;
            _servRegion = servRegion;
            _logger = factory.CreateLogger<AdminEdificioController>();
            _mapper = mapper;
            _servProvincia = servProvincia;
            _servTipoUso = servTipoUso;
        }

        private void ObtenerData()
        {
            ViewBag.TipoEdificioList = _servTipoEdificio.All();
            ViewBag.Comunas = SelectHelper.LlenarDDL(_servComuna.All(), true);
            ViewBag.Regiones = SelectHelper.LlenarDDL(_servRegion.All(), true);
            ViewBag.Provincias = SelectHelper.LlenarDDL(_servProvincia.All(), true);
            ViewBag.TiposUso = SelectHelper.LlenarDDL(_servTipoUso.All(), true);
        }

        private void ObtenerDataEdit()
        {
            ViewBag.TipoEdificioList = _servTipoEdificio.All();
            ViewBag.Regiones = SelectHelper.LlenarDDL(_servRegion.All(), true);
            ViewBag.TiposUso = SelectHelper.LlenarDDL(_servTipoUso.All(), true);
        }

        // GET: EdificioModels
        public async Task<IActionResult> Index(EdificioRequest request)
        {
            ViewData["permisos"] = GetPermisions();

            var indexModel = new EdificioIndexModel();
            indexModel.Items = _servEdificio.Paged(request);
            indexModel.Sort = SortItems();

            ObtenerData();


            return View(indexModel);
        }

        // GET: EdificioModels/Details/5
        public async Task<IActionResult> Details(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edificioModel = _servEdificio.Get(id);
            if (edificioModel == null)
            {
                return NotFound();
            }

            return View(edificioModel);
        }

        // GET: EdificioModels/Create
        public IActionResult Create()
        {
            if (!permisoAccion.Escritura)
            {
                return Unauthorized();
            }

            ObtenerData();

            //ViewBag.Mensaje = StatusMessage;

            return View();
        }

        // POST: EdificioModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Calle,Numero,ComunaId")] EdificioModel edificioModel)
        {
            if (!ModelState.IsValid)
            {
                return View(edificioModel);
            }

            var result = _servEdificio.Insert(edificioModel);

            //StatusMessage = result > 0 ? "Edificio fue creada satisfactoriamente" : "Error al crear edificio";

            return RedirectToAction(nameof(Index));
        }

        // GET: EdificioModels/Edit/5
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

            var edificioModel = _servEdificio.Get(id);
            if (edificioModel == null)
            {
                return NotFound();
            }
            //ObtenerData();
            ObtenerDataEdit();
            edificioModel.ModifiedBy = NombreCompletoUsuario(edificioModel.ModifiedBy);
            edificioModel.CreatedBy = NombreCompletoUsuario(edificioModel.CreatedBy);

            ViewBag.TiposDeUso = SelectHelper.LlenarDDL(_servTipoUso.getByEdificioId(id)); 

            return View(edificioModel);
        }

        // POST: EdificioModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,Calle,Numero,ComunaId, RegionId, ProvinciaId")] EdificioModel edificioModel)
        {
            if (id != edificioModel.Id)
            {
                return NotFound();
            }


            if (!ModelState.IsValid)
            {
                ObtenerDataEdit();
                edificioModel.ModifiedBy = NombreCompletoUsuario(edificioModel.ModifiedBy);
                edificioModel.CreatedBy = NombreCompletoUsuario(edificioModel.CreatedBy);

                ViewBag.TiposDeUso = SelectHelper.LlenarDDL(_servTipoUso.getByEdificioId(id));
                //return View(nameof(Edit), edificioModel);
                return BadRequest(ModelState);
            }
            

            try
            {
                _servEdificio.Update(edificioModel);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EdificioModelExists(edificioModel.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            //return RedirectToAction(nameof(Index));
            return Ok();
        }

        // GET: EdificioModels/Delete/5
        public async Task<IActionResult> Delete(long id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var edificioModel = _servEdificio.Get(id);
            if (edificioModel == null)
            {
                return NotFound();
            }

            return View(edificioModel);
        }

        // POST: EdificioModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            _servEdificio.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        private bool EdificioModelExists(long id)
        {
            return (_servEdificio.Get(id) == null) ? false : true;
        }

        private SortModel SortItems()
        {
            var order = base.SortItems();
            order.AddSortField("Id", "id");
            order.AddSortField("Dirección", "direccion");
            return order;
        }
    }
}
