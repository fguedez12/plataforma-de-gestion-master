using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Configuration
{
    public class ServiceConfiguration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IDivisionService, DivisionService>();
            services.AddScoped<IEnergeticoService, EnergeticoService>();
            services.AddScoped<IEstadoValidacionService, EstadoValidacionService>();
            services.AddScoped<IEdificioService, EdificioService>();
            services.AddScoped<IEnergeticoDivisionService, EnergeticoDivisionService>();
            services.AddScoped<INumeroClienteService, NumeroClienteService>();
            services.AddScoped<IEmpresaDistribuidoraService, EmpresaDistribuidoraService>();
            services.AddScoped<ITipoTarifaService, TipoTarifaService>();
            services.AddScoped<IMedidorService, MedidorService>();
            services.AddScoped<ITipoUsoService, TipoUsoService>();
            services.AddScoped<ITipoPropiedadService, TipoPropiedadService>();
            services.AddScoped<IEnergeticoUnidadMedidaService, EnergeticoUnidadMedidaService>();
            services.AddScoped<IUnidadMedidaService, UnidadMedidaService>();
            services.AddScoped<ICompraService, CompraService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IRolService, RolService>();
            services.AddScoped<IServicioService, ServicioService>();
            services.AddScoped<IArchivoAdjuntoService, ArchivoAdjuntoService>();
            services.AddScoped<IInstitucionService, InstitucionService>();
            services.AddScoped<IRegionService, RegionService>();
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPermisosService, PermisosService>();
            services.AddScoped<IReporteService, ReporteService>();
            services.AddScoped<ITipoEdificioService, TipoEdificioService>();
            services.AddScoped<IComunaService, ComunaService>();
            services.AddScoped<ITipoAgrupamientoService, TipoAgrupamientoService>();
            services.AddScoped<IEntornoService, EntornoService>();
            services.AddScoped<IInerciaTermicaService, InerciaTermicaService>();
            services.AddScoped<ITipoUnidadService, TipoUnidadService>();
            services.AddScoped<ISexoService, SexoService>();
            services.AddScoped<IProvinciaService, ProvinciaService>();
            services.AddScoped<IParametroMedicionService, ParametroMedicionService>();
            services.AddScoped<IMedidorDivisionService, MedidorDivisionService>();
            services.AddScoped<ITipoNivelPisoService, TipoNivelPisoService>();
            services.AddScoped<INumeroPisoService, NumeroPisoService>();
            services.AddScoped<IPisoService, PisoService>();
            services.AddScoped<IDisenioPasivoService, DisenioPasivoService>();
            services.AddScoped<ITrazabilidadService, TrazabilidadService>();
            services.AddScoped<IMuroService, MuroService>();
            services.AddScoped<ITipoSombreadoService, TipoSombreadoService>();
            services.AddScoped<IEstructuraService, EstructuraService>();
            services.AddScoped<ICertificadoService, CertificadoService>();
            services.AddScoped<IAMedidorService, AMedidorService>();
            services.AddScoped<IInmuebleService, InmuebleService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IUnidadService, UnidadService>();
            services.AddScoped<IMiUnidadService, MiUnidadService>();
        }
    }
}
