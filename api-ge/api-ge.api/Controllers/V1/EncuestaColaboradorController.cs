using api_gestiona.DTOs;
using api_gestiona.DTOs.EncuestaColaborador;
using api_gestiona.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona.Controllers.V1
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EncuestaColaboradorController : CustomController
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EncuestaColaboradorController(ApplicationDbContext context, IMapper mapper, UserManager<Usuario> manager) : base(mapper, context, manager)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("{servicioId}/{year}")]
        public async Task<ActionResult> Get([FromRoute] long servicioId, [FromRoute] int year)
        {
            var exist = _context.EncuestaColaboradores.Any(x => x.ServicioId == servicioId && x.Year == year);
            if (!exist)
            {
                var newEncuesta = new EncuestaColaborador() { ServicioId = servicioId, Year = year };
                _context.EncuestaColaboradores.Add(newEncuesta);
                await _context.SaveChangesAsync();
                var responseNew = _mapper.Map<EncuestaColaboradorResponseDTO>(newEncuesta);
                return Ok(responseNew);
            }

            var encuesta = await _context.EncuestaColaboradores.FirstOrDefaultAsync(x => x.ServicioId == servicioId && x.Year == year);
            var response = _mapper.Map<EncuestaColaboradorResponseDTO>(encuesta);
            response.VehiculoPropio = _mapper.Map<VehiculoPropioDTO>(encuesta);
            response.VehiculoCompartido = _mapper.Map<VehiculoCompartidoDTO>(encuesta);
            response.TransportePublico = _mapper.Map<TransportePublicoDTO>(encuesta);
            response.Bicicleta = _mapper.Map<BicicletaDTO>(encuesta);
            response.Motocicleta = _mapper.Map<MotocicletaDTO>(encuesta);
            response.OtrasFormas = _mapper.Map<OtrasFormasDTO>(encuesta);
            return Ok(response);

        }

        [HttpPost("vehiculo-propio/{servicioId}/{year}")]
        public async Task<ActionResult> postVehiculoPropio([FromRoute] long servicioId, VehiculoPropioDTO model,[FromRoute] int year)
        {
            var entity = await _context.EncuestaColaboradores.FirstOrDefaultAsync(x => x.ServicioId == servicioId && x.Year == year);
            if (entity == null)
            {
                return Ok(new ErrorResponse { Error = true, Msj = "el rescurso solicitado no existe" });
            }

            entity.TotalEncuestados = model.TotalEncuestados;
            entity.C1HibP = model.C1HibP;
            entity.C2HibP = model.C2HibP;
            entity.C3HibP = model.C3HibP;
            entity.C4HibP = model.C4HibP;
            entity.C4mHibP = model.C4mHibP;
            entity.D1HibP = model.D1HibP;
            entity.D2HibP = model.D2HibP;
            entity.D3HibP = model.D3HibP;
            entity.D4HibP = model.D4HibP;
            entity.D4mHibP = model.D4mHibP;
            entity.C1HidP = model.C1HidP;
            entity.C2HidP = model.C2HidP;
            entity.C3HidP = model.C3HidP;
            entity.C4HidP = model.C4HidP;
            entity.C4mHidP = model.C4mHidP;
            entity.D1HidP = model.D1HidP;
            entity.D2HidP = model.D2HidP;
            entity.D3HidP = model.D3HidP;
            entity.D4HidP = model.D4HidP;
            entity.D4mHidP = model.D4mHidP;
            entity.C1ElP = model.C1ElP;
            entity.C2ElP = model.C2ElP;
            entity.C3ElP = model.C3ElP;
            entity.C4ElP = model.C4ElP;
            entity.C4mElP = model.C4mElP;
            entity.D1ElP = model.D1ElP;
            entity.D2ElP = model.D2ElP;
            entity.D3ElP = model.D3ElP;
            entity.D4ElP = model.D4ElP;
            entity.D4mElP = model.D4mElP;
            entity.C1DisP = model.C1DisP;
            entity.C2DisP = model.C2DisP;
            entity.C3DisP = model.C3DisP;
            entity.C4DisP = model.C4DisP;
            entity.C4mDisP = model.C4mDisP;
            entity.D1DisP = model.D1DisP;
            entity.D2DisP = model.D2DisP;
            entity.D3DisP = model.D3DisP;
            entity.D4DisP = model.D4DisP;
            entity.D4mDisP = model.D4mDisP;
            entity.C1GasP = model.C1GasP;
            entity.C2GasP = model.C2GasP;
            entity.C3GasP = model.C3GasP;
            entity.C4GasP = model.C4GasP;
            entity.C4mGasP = model.C4mGasP;
            entity.D1GasP = model.D1GasP;
            entity.D2GasP = model.D2GasP;
            entity.D3GasP = model.D3GasP;
            entity.D4GasP = model.D4GasP;
            entity.D4mGasP = model.D4mGasP;

            _context.EncuestaColaboradores.Update(entity);
            await _context.SaveChangesAsync();
            return Ok("Ok");
        }

        [HttpPost("vehiculo-compartido/{servicioId}/{year}")]
        public async Task<ActionResult> postVehiculoCompartido([FromRoute] long servicioId, VehiculoCompartidoDTO model,[FromRoute] int year)
        {
            var entity = await _context.EncuestaColaboradores.FirstOrDefaultAsync(x => x.ServicioId == servicioId && x.Year == year);
            if (entity == null)
            {
                return Ok(new ErrorResponse { Error = true, Msj = "el rescurso solicitado no existe" });
            }

            entity.TotalEncuestados = model.TotalEncuestados;
            entity.C1HibC = model.C1HibC;
            entity.C2HibC = model.C2HibC;
            entity.C3HibC = model.C3HibC;
            entity.C4HibC = model.C4HibC;
            entity.C4mHibC = model.C4mHibC;
            entity.D1HibC = model.D1HibC;
            entity.D2HibC = model.D2HibC;
            entity.D3HibC = model.D3HibC;
            entity.D4HibC = model.D4HibC;
            entity.D4mHibC = model.D4mHibC;
            entity.C1HidC = model.C1HidC;
            entity.C2HidC = model.C2HidC;
            entity.C3HidC = model.C3HidC;
            entity.C4HidC = model.C4HidC;
            entity.C4mHidC = model.C4mHidC;
            entity.D1HidC = model.D1HidC;
            entity.D2HidC = model.D2HidC;
            entity.D3HidC = model.D3HidC;
            entity.D4HidC = model.D4HidC;
            entity.D4mHidC = model.D4mHidC;
            entity.C1ElC = model.C1ElC;
            entity.C2ElC = model.C2ElC;
            entity.C3ElC = model.C3ElC;
            entity.C4ElC = model.C4ElC;
            entity.C4mElC = model.C4mElC;
            entity.D1ElC = model.D1ElC;
            entity.D2ElC = model.D2ElC;
            entity.D3ElC = model.D3ElC;
            entity.D4ElC = model.D4ElC;
            entity.D4mElC = model.D4mElC;
            entity.C1DisC = model.C1DisC;
            entity.C2DisC = model.C2DisC;
            entity.C3DisC = model.C3DisC;
            entity.C4DisC = model.C4DisC;
            entity.C4mDisC = model.C4mDisC;
            entity.D1DisC = model.D1DisC;
            entity.D2DisC = model.D2DisC;
            entity.D3DisC = model.D3DisC;
            entity.D4DisC = model.D4DisC;
            entity.D4mDisC = model.D4mDisC;
            entity.C1GasC = model.C1GasC;
            entity.C2GasC = model.C2GasC;
            entity.C3GasC = model.C3GasC;
            entity.C4GasC = model.C4GasC;
            entity.C4mGasC = model.C4mGasC;
            entity.D1GasC = model.D1GasC;
            entity.D2GasC = model.D2GasC;
            entity.D3GasC = model.D3GasC;
            entity.D4GasC = model.D4GasC;
            entity.D4mGasC = model.D4mGasC;

            _context.EncuestaColaboradores.Update(entity);
            await _context.SaveChangesAsync();
            return Ok("Ok");
        }

        [HttpPost("transporte-publico/{servicioId}/{year}")]
        public async Task<ActionResult> postTransportePublico([FromRoute] long servicioId, TransportePublicoDTO model,[FromRoute] int year)
        {
            var entity = await _context.EncuestaColaboradores.FirstOrDefaultAsync(x => x.ServicioId == servicioId && x.Year == year);
            if (entity == null)
            {
                return Ok(new ErrorResponse { Error = true, Msj = "el rescurso solicitado no existe" });
            }

            entity.TotalEncuestados = model.TotalEncuestados;
            entity.CBusIu = model.CBusIu;
            entity.DBusIu = model.DBusIu;
            entity.CBusL = model.CBusL;
            entity.DBusL = model.DBusL;
            entity.CBusTs = model.CBusTs;
            entity.DBusTs = model.DBusTs;
            entity.CMetro = model.CMetro;
            entity.DMetro = model.DMetro;
            entity.CMetroT = model.CMetroT;
            entity.DMetroT = model.DMetroT;
            entity.CMetroTc = model.CMetroTc;
            entity.DMetroTc = model.DMetroTc;
            entity.CTpeA = model.CTpeA;
            entity.DTpeA = model.DTpeA;
            entity.CTpeM = model.CTpeM;
            entity.DTpeM = model.DTpeM;

            _context.EncuestaColaboradores.Update(entity);
            await _context.SaveChangesAsync();
            return Ok("Ok");
        }

        [HttpPost("bicicleta/{servicioId}/{year}")]
        public async Task<ActionResult> postBicicleta([FromRoute] long servicioId, BicicletaDTO model,[FromRoute] int year)
        {
            var entity = await _context.EncuestaColaboradores.FirstOrDefaultAsync(x => x.ServicioId == servicioId  && x.Year == year);
            if (entity == null)
            {
                return Ok(new ErrorResponse { Error = true, Msj = "el rescurso solicitado no existe" });
            }

            entity.TotalEncuestados = model.TotalEncuestados;
            entity.CBiciTa = model.CBiciTa;
            entity.DBiciTa = model.DBiciTa;
            entity.CBiciEs = model.CBiciEs;
            entity.DBiciEs = model.DBiciEs;
            entity.CBiciAd = model.CBiciAd;
            entity.DBiciAd = model.DBiciAd;

            _context.EncuestaColaboradores.Update(entity);
            await _context.SaveChangesAsync();
            return Ok("Ok");
        }
        [HttpPost("motocicleta/{servicioId}/{year}")]
        public async Task<ActionResult> postMotocicleta([FromRoute] long servicioId, MotocicletaDTO model,[FromRoute] int year)
        {
            var entity = await _context.EncuestaColaboradores.FirstOrDefaultAsync(x => x.ServicioId == servicioId && x.Year == year);
            if (entity == null)
            {
                return Ok(new ErrorResponse { Error = true, Msj = "el rescurso solicitado no existe" });
            }

            entity.TotalEncuestados = model.TotalEncuestados;
            entity.CMotoEl = model.CMotoEl;
            entity.DMotoEl = model.DMotoEl;
            entity.CMotoGas = model.CMotoGas;
            entity.DMotoGas = model.DMotoGas;

            _context.EncuestaColaboradores.Update(entity);
            await _context.SaveChangesAsync();
            return Ok("Ok");
        }
        [HttpPost("otra/{servicioId}/{year}")]
        public async Task<ActionResult> postOtrasFormas([FromRoute] long servicioId, OtrasFormasDTO model,[FromRoute] int year)
        {
            var entity = await _context.EncuestaColaboradores.FirstOrDefaultAsync(x => x.ServicioId == servicioId && x.Year == year);
            if (entity == null)
            {
                return Ok(new ErrorResponse { Error = true, Msj = "el rescurso solicitado no existe" });
            }

            entity.TotalEncuestados = model.TotalEncuestados;
            entity.CTaxi = model.CTaxi;
            entity.DTaxi = model.DTaxi;
            entity.CColectivo = model.CColectivo;
            entity.DColectivo = model.DColectivo;
            entity.CCaminando = model.CCaminando;
            entity.DCaminando = model.DCaminando;
            entity.CScooterEl = model.CScooterEl;
            entity.DScooterEl = model.DScooterEl;
            entity.CBiciEl = model.CBiciEl;
            entity.DBiciEl = model.DBiciEl;

            _context.EncuestaColaboradores.Update(entity);
            await _context.SaveChangesAsync();
            return Ok("Ok");
        }

    }
}
