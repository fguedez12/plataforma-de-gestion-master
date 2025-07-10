using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Data;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.CompraModels;
using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using Microsoft.Extensions.Logging;
using GobEfi.Web.Models.ConsumoModels;
using GobEfi.Web.Models.EnergeticoModels;
using GE.Models.ParametrosMedicionModels;
using GobEfi.Web.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using GobEfi.Web.Core.Contracts.Repositories;
using AutoMapper.QueryableExtensions;
using GobEfi.Web.Services.Request;
using GobEfi.Web.Services;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace GobEfi.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComprasController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly ICompraService _servCompra;
        private readonly IUsuarioService _servUsuario;
        private readonly IEmailSender _emailSender;
        private readonly IEnergeticoService _servEnergetico;
        private readonly ILogger _logger;

        public ComprasController( IMapper mapper, ICompraService servCompra, ILoggerFactory loggerFactory,
            ApplicationDbContext context,
            IUsuarioService servUsuario,
            UserManager<Usuario> manager,
            IHttpContextAccessor httpContextAccessor,
            IEmailSender emailSender,
            IEnergeticoService servEnergetico) : base(context, manager, httpContextAccessor, servUsuario)
        {
            _mapper = mapper;
            _servCompra = servCompra;
            _servUsuario = servUsuario;
            this._emailSender = emailSender;
            _servEnergetico = servEnergetico;
            _logger = loggerFactory.CreateLogger<ComprasController>();
        }

        //GET: api/Compras/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompras([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var compraParaRetornar = await _servCompra.GetParaRetornar(id);

            if (compraParaRetornar == null)
            {
                return NotFound();
            }

            compraParaRetornar.ModifiedBy = _servUsuario.NombreCompletoUsuario(compraParaRetornar.ModifiedBy);

            return Ok(compraParaRetornar);
            //return Ok(compra);
        }

        [HttpGet("ParaValidar/{compraId}")]
        public async Task<IActionResult> GetComprasParaValidar([FromRoute] long compraId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                CompraParaValidarDetalleModel compra = await _servCompra.GetParaValidar(compraId);


                if (compra == null)
                {
                    return NotFound();
                }

                return Ok(compra);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        // GET: api/Compras/byDivision/5
        [HttpGet("byDivision/{divisionId}/{filtro}")]
        public async Task<IActionResult> GetComprasByDivision([FromRoute] long divisionId, [FromRoute] int? filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                bool tienePermisos = await _servCompra.ValidaPermiso(divisionId);

                if (!tienePermisos)
                    return Unauthorized();

                var energeticosActivos = (await _servEnergetico.GetActivosByDivision(divisionId)).ToList();

                SubMenuConsumoModel subMenu = new SubMenuConsumoModel
                {
                    EnergeticoSubMenu = energeticosActivos,
                    AniosSubMenu = new List<int>()
                };

                var compras = _servCompra.ObtenerLasCompras(divisionId, energeticosActivos, filtro);

                //var anios = context.Compras.Where(x=>x.DivisionId == divisionId && x.Active && x.FechaCompra> new DateTime(1900, 1, 1))
                //                .Select(a => a.FechaCompra.Year).Distinct().ToList();
                //var aniosMed = context.Compras.Where(x => x.DivisionId == divisionId && x.Active)
                //        .Select(a => a.InicioLectura.Year).Distinct().ToList();

                //anios.AddRange(aniosMed);

                //anios = anios.Distinct().ToList();

                var listAnios = new List<int>();
                for (int i = 0; i < 7; i++)
                {
                    listAnios.Add(DateTime.Now.Year-i);
                }

                

                //var anioActual = DateTime.Now.Year;
                //if (!anios.Any(x=>x==anioActual)) {
                //    anios.Add(anioActual);
                //}

                //var anios = compras.Where(fe => fe.FechaCompra > new DateTime(1900, 1, 1)).Select(a => a.FechaCompra.Year).Distinct().ToList();
                //anios.AddRange(compras
                //        .Where(fe => fe.InicioLectura > new DateTime(1900, 1, 1))
                //        .Select(fe => fe.InicioLectura.Year).Distinct().ToList());


                subMenu.AniosSubMenu = listAnios;

                ConsumoModel consumo = new ConsumoModel
                {
                    SubMenu = subMenu,
                    Compras = compras

                };

                return Ok(consumo);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            } 
        }

        [HttpGet("getComprasParaValidar")]
        public async Task<IActionResult> GetComprasParaValidar([FromQuery] CompraParaValidarRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                IEnumerable<CompraParaValidarModel> compras = await _servCompra.getComprasParaValidar(request);

                return Ok(compras);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }

        // PUT: api/Compras/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompra([FromRoute] long id, [FromBody] CompraForEdit compraFromBody)
        {


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                
                var ret = await _servCompra.Update(id, compraFromBody);

                return NoContent();
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
 
        }

        [HttpPost("{compraId}/accion/{accion}")]
        public async Task<IActionResult> PostAccionEstado([FromRoute] long compraId, [FromRoute] string accion, [FromForm] string obs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                string observ = obs;

                string usuario = await _servCompra.AccionEstado(compraId, accion, observ);

                var compra = _servCompra.Get(compraId);
                string userCompra;
                if (string.IsNullOrWhiteSpace(compra.ModifiedBy))
                {
                    userCompra = compra.CreatedBy;
                }
                else 
                {
                    userCompra = compra.ModifiedBy;
                }

                if (!string.IsNullOrWhiteSpace(userCompra) && accion == "o")
                {
                    var usuarioModificacion = _servUsuario.Get(userCompra);
                    var usuarioactual = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
                    var body = EmailBodyObservations(_servUsuario.NombreCompletoUsuario(usuarioactual), compra.Id, compra.ObservacionRevision);
                    
                    await _emailSender.SendEmailAsync(usuarioModificacion.Email, "Modificación de compra", body);
                }
                return Ok(usuario);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        private string EmailBodyObservations(string nombreGestor, long compraId, string obs)
        {
            string body = $"Estimado Gestor,<br>Se envía el siguiente mail para notificar que con fecha {DateTime.Now.ToString("dd-MM-yyyy")}," +
                            $" el usuario <b>{nombreGestor}</b><br> ha realizado una observación en la compra número <b>{compraId}</b>.<br>" +
                            $"La observación realizada corresponde a : '{obs}'<br><br><br> Atte. <br>Equipo Gestiona Energía."
                            ;
            return body;
        }

        // POST: api/Compras
        [HttpPost]
        public async Task<IActionResult> PostCompra([FromBody] CompraForRegister compra)
        {

            //Cambio temporal para energeticos sin medidor

            if (compra.ConsumoCompra == null)
            {
                compra.FechaCompra = DateTime.Now.ToString();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                long newId = await _servCompra.Add(compra);

                compra.Id = newId;

                return CreatedAtAction("GetCompra", new { id = compra.Id }, compra);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

           
        }

        // DELETE: api/Compras/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompra([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var compra = _servCompra.Get(id);
                if (compra == null)
                {
                    return NotFound();
                }

                int result = await _servCompra.DeleteAsync(id);

                return Ok(compra);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


    }
}