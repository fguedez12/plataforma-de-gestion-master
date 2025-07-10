using GobEfi.Web.Data.Repositories;
using GobEfi.Web.Core.Contracts.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Configuration
{
    public class RepositoryConfiguration
    {
        public static void Register(IServiceCollection services)
        {
            services.AddScoped<IDivisionRepository, DivisionRepository>();
            services.AddScoped<IEnergeticoRepository, EnergeticoRepository>();
            services.AddScoped<IEstadoValidacionRepository, EstadoValidacionRepository>();
            services.AddScoped<IEdificioRepository, EdificioRepository>();
            services.AddScoped<IEnergeticoDivisionRepository, EnergeticoDivisionRepository>();
            services.AddScoped<INumeroClienteRepository, NumeroClienteRepository>();
            services.AddScoped<IEmpresaDistribuidoraRepository, EmpresaDistribuidoraRepository>();
            services.AddScoped<ITipoTarifaRepository, TipoTarifaRepository>();
            services.AddScoped<IMedidorRepository, MedidorRepository>();
            services.AddScoped<ITipoUsoRepository, TipoUsoRepository>();
            services.AddScoped<ITipoPropiedadRepository, TipoPropiedadRepository>();
            services.AddScoped<IEnergeticoUnidadMedidaRepository, EnergeticoUnidadMedidaRepository>();
            services.AddScoped<IUnidadMedidaRepository, UnidadMedidaRepository>();
            services.AddScoped<ICompraRepository, CompraRepository>();
            services.AddScoped<ITipoArchivoRepository, TipoArchivoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IRolRepository, RolRepository>();
            services.AddScoped<IServicioRepository, ServicioRepository>();
            services.AddScoped<IArchivoAdjuntoRepository, ArchivoAdjuntoRepository>();
            services.AddScoped<ICompraMedidorRepository, CompraMedidorRepository>();
            services.AddScoped<IInstitucionRepository, InstitucionRepository>();
            services.AddScoped<IRegionRepository, RegionRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IPermisosRepository, PermisosRepository>();
            services.AddScoped<IReporteRepository, ReporteRepository>();
            services.AddScoped<IReporteRolRepository, ReportesRolRepository>();
            services.AddScoped<ITipoEdificioRepository, TipoEdificioRepository>();
            services.AddScoped<IComunaRepository, ComunaRepository>();
            services.AddScoped<ITipoAgrupamientoRepository, TipoAgrupamientoRepository>();
            services.AddScoped<IEntornoRepository, EntornoRepository>();
            services.AddScoped<IInerciaTermicaRepository, InerciaTermicaRepository>();
            services.AddScoped<ITipoUnidadRepository, TipoUnidadRepository>();
            services.AddScoped<ISexoRepository, SexoRepository>();
            services.AddScoped<IProvinciaRepository, ProvinciaRepository>();
            services.AddScoped<IParametroMedicionRepository, ParametroMedicionRepository>();
            services.AddScoped<IUsuarioDivisionRepository, UsuarioDivisionRepository>();
            services.AddScoped<IUsuarioServicioRepository, UsuarioServicioRepository>();
            services.AddScoped<IMedidorDivisionRepository, MedidorDivisionRepository>();
            services.AddScoped<INumeroPisoRepository, NumeroPisoRepository>();
            services.AddScoped<IPisoRepository, PisoRepository>();
            services.AddScoped<ITrazabilidadesRepository, TrazabilidadesRepository>();

        }
    }
}
