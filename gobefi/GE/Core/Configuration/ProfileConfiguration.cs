using AutoMapper;
using GobEfi.Web.Core.Profiles;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.Web.Core.Configuration
{
    public class ProfileConfiguration
    {
        public static void Register(IServiceCollection services)
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<EstadoValidacionProfile>();
                cfg.AddProfile<EdificioProfile>();
                cfg.AddProfile<DivisionProfile>();
                cfg.AddProfile<EnergeticoProfile>();
                cfg.AddProfile<EnergeticoDivisionProfile>();
                cfg.AddProfile<TipoUsoProfile>();
                cfg.AddProfile<NumeroClienteProfile>();
                cfg.AddProfile<EmpresaDistribuidoraProfile>();
                cfg.AddProfile<TipoTarifaProfile>();
                cfg.AddProfile<MedidorProfile>();
                cfg.AddProfile<EnergeticoUnidadMedidaProfile>();
                cfg.AddProfile<UnidadMedidaProfile>();
                cfg.AddProfile<CompraProfile>();
                cfg.AddProfile<ArchivoAdjuntoProfile>();
                cfg.AddProfile<ComunaProfile>();
                cfg.AddProfile<TipoEdificioProfile>();
                cfg.AddProfile<TipoPropiedadProfile>();
                cfg.AddProfile<UsuarioProfile>();
                cfg.AddProfile<RolProfile>();
                cfg.AddProfile<ParametrosMedicionProfile>();
                cfg.AddProfile<CompraMedidorProfile>();
                cfg.AddProfile<ServicioProfile>();
                cfg.AddProfile<InstitucionProfile>();
                cfg.AddProfile<MenuPanelProfile>();
                cfg.AddProfile<MenuProfile>();
                cfg.AddProfile<PermisosProfile>();
                cfg.AddProfile<TipoUnidadProfile>();
                cfg.AddProfile<TipoAgrupamientoProfile>();
                cfg.AddProfile<TipoArchivoProfile>();
                cfg.AddProfile<SexoProfile>();
                cfg.AddProfile<ProvinciaProfile>();
                cfg.AddProfile<ReporteProfile>();
                cfg.AddProfile<MedidorDivisionProfile>();
                cfg.AddProfile<EntornoProfile>();
                cfg.AddProfile<InerciaTermicaProfile>();
                cfg.AddProfile<NumeroPisoProfile>();
                cfg.AddProfile<PisoProfile>();
                cfg.AddProfile<MuroProfile>();
                cfg.AddProfile<TipoSombreadoProfile>();
                cfg.AddProfile<TechoProfile>();
                cfg.AddProfile<ArchivoDpProfile>();
                cfg.AddProfile<VentanaProfile>();
                cfg.AddProfile<CertificadoProfile>();
                cfg.AddProfile<EstructuraProfile>();
                cfg.AddProfile<AMedidorProfile>();
                cfg.AddProfile<InmuebleProfile>();
                cfg.AddProfile<AreaProfile>();
                cfg.AddProfile<UnidadProfile>();
                cfg.AddProfile<DireccionProfile>();
                cfg.AddProfile<AjustesProfile>();
                cfg.AddProfile<RegistroProfile>();
            });

            var mapper = config.CreateMapper();
            services.AddSingleton<IMapper>(mapper);
            services.AddAutoMapper();
        }
    }
}
