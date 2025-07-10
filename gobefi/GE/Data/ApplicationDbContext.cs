using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GobEfi.Web.Core.Contracts;
using GobEfi.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GobEfi.Web.Models.TipoUsoModels;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Models.TipoPropiedadModels;
using GobEfi.Web.Models.UsuarioModels;
using GobEfi.Web.Models.RolModels;
using GobEfi.Web.Models.ServicioModels;
using GobEfi.Web.Models.EdificioModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using GobEfi.Web.Models.InstitucionModels;

namespace GobEfi.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, Rol, string, IdentityUserClaim<string>, UsuarioRol, IdentityUserLogin<string>,
        IdentityRoleClaim<string>, IdentityUserToken<string>>
    {

        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //builder.Entity<NotasCertificado>(b=>
            //    b.HasKey(k=> new { k.CertificadoId,k.UsuarioId})
            //    );

            builder.Entity<Usuario>(b =>
            {
                // Each User can have many UserClaims
                b.HasMany(e => e.Claims)
                    .WithOne()
                    .HasForeignKey(uc => uc.UserId)
                    .IsRequired();
                
                // Each User can have many UserLogins
                b.HasMany(e => e.Logins)
                    .WithOne()
                    .HasForeignKey(ul => ul.UserId)
                    .IsRequired();
                
                // Each User can have many UserTokens
                b.HasMany(e => e.Tokens)
                    .WithOne()
                    .HasForeignKey(e => e.UserId)
                    .IsRequired();

                // Each User can have many entries in the UserRole join table
                b.HasMany(e => e.UsuarioRoles)
                    .WithOne()
                    .HasForeignKey(e => e.UserId)
                    .IsRequired();
            });

            builder.Entity<Rol>(b =>
            {
                b.HasMany(e => e.UsuarioRoles)
                    .WithOne()
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
            });

            builder.Entity<UsuarioDivision>()
                .HasKey(k => new { k.DivisionId, k.UsuarioId });
            builder.Entity<UsuarioDivision>()
                .HasOne(ud => ud.Division)
                .WithMany(u => u.UsuariosDivisiones)
                .HasForeignKey(ud => ud.DivisionId);
            builder.Entity<UsuarioDivision>()
                .HasOne(ud => ud.Usuario)
                .WithMany(u => u.UsuariosDivisiones)
                .HasForeignKey(ud => ud.UsuarioId);

            builder.Entity<UsuarioInstitucion>()
                .HasKey(k => new { k.InstitucionId, k.UsuarioId });
            builder.Entity<UsuarioInstitucion>()
                .HasOne(ud => ud.Institucion)
                .WithMany(u => u.UsuariosInstituciones)
                .HasForeignKey(ud => ud.InstitucionId);
            builder.Entity<UsuarioInstitucion>()
                .HasOne(ud => ud.Usuario)
                .WithMany(u => u.UsuariosInstituciones)
                .HasForeignKey(ud => ud.UsuarioId);

            builder.Entity<UsuarioServicio>()
                .HasKey(k => new { k.ServicioId, k.UsuarioId });
            builder.Entity<UsuarioServicio>()
                .HasOne(ud => ud.Servicio)
                .WithMany(u => u.UsuariosServicios)
                .HasForeignKey(ud => ud.ServicioId);
            builder.Entity<UsuarioServicio>()
                .HasOne(ud => ud.Usuario)
                .WithMany(u => u.UsuariosServicios)
                .HasForeignKey(ud => ud.UsuarioId);

            builder.Entity<CompraMedidor>()
                .HasOne(m => m.Medidor)
                .WithMany(cm => cm.CompraMedidor)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ArchivoAdjunto>()
                .HasOne(a => a.Compra)
                .WithOne(b => b.Factura)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Compra>()
                .Property(b => b.EstadoValidacionId)
                .HasDefaultValue("sin_revision");

            builder.Entity<Division>()
                .Property(d => d.PisosIguales)
                .HasDefaultValue(true);

            builder.Entity<Muro>()
                .Property(m => m.Latitud)
                .HasColumnType("decimal(18,15)");
            builder.Entity<Muro>()
             .Property(m => m.Longitud)
             .HasColumnType("decimal(18,15)");

            //builder.Entity<Permisos>()
            //    .HasOne(s => s.SubMenu)
            //    .WithMany(d => d.Permisos)
            //    .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<EstructuraAislacion>()
                .HasKey(k => new { k.EstructuraId, k.AislacionId });
            builder.Entity<EstructuraMaterialidad>()
                .HasKey(k => new { k.EstructuraId, k.MaterialidadId });
            builder.Entity<EstructuraTipoSombreado>()
               .HasKey(k => new { k.EstructuraId, k.TipoSombreadoId });
            builder.Entity<EstructuraPosicionVentana>()
               .HasKey(k => new { k.EstructuraId, k.PosicionVentanaId });

            builder.Entity<Ventana>()
                .HasOne(v => v.TipoCierre)
                .WithMany(a => a.VentanasTipoCierre)
                .HasForeignKey(v => v.TipoCierreId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Ventana>()
                .HasOne(v => v.TipoMarco)
                .WithMany(a => a.VentanasTipoMarco)
                .HasForeignKey(v => v.TipoMarcoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UnidadInmueble>()
               .HasKey(ui => new { ui.UnidadId, ui.InmuebleId });
            builder.Entity<UnidadInmueble>()
                .HasOne(ui => ui.Unidad)
                .WithMany(ui => ui.UnidadInmuebles)
                .HasForeignKey(ui => ui.UnidadId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<UnidadInmueble>()
             .HasOne(ui => ui.Inmueble)
             .WithMany(ui => ui.UnidadInmuebles)
             .HasForeignKey(ui => ui.InmuebleId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UnidadPiso>()
               .HasKey(ui => new { ui.UnidadId, ui.PisoId });
            builder.Entity<UnidadPiso>()
                .HasOne(ui => ui.Unidad)
                .WithMany(ui => ui.UnidadPisos)
                .HasForeignKey(ui => ui.UnidadId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<UnidadPiso>()
             .HasOne(ui => ui.Piso)
             .WithMany(ui => ui.UnidadPisos)
             .HasForeignKey(ui => ui.PisoId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UnidadArea>()
              .HasKey(ui => new { ui.UnidadId, ui.AreaId });
            builder.Entity<UnidadArea>()
                .HasOne(ui => ui.Unidad)
                .WithMany(ui => ui.UnidadAreas)
                .HasForeignKey(ui => ui.UnidadId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<UnidadArea>()
             .HasOne(ui => ui.Area)
             .WithMany(ui => ui.UnidadAreas)
             .HasForeignKey(ui => ui.AreaId)
             .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Direccion>()
                .HasOne(x=>x.Region)
                .WithMany(r => r.Direcciones)
                .HasForeignKey(x => x.RegionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<UsuarioUnidad>()
               .HasKey(k => new { k.UnidadId, k.UsuarioId });
            builder.Entity<UsuarioUnidad>()
                .HasOne(ud => ud.Unidad)
                .WithMany(u => u.UsuarioUnidades)
                .HasForeignKey(ud => ud.UnidadId);
            builder.Entity<UsuarioUnidad>()
                .HasOne(ud => ud.Usuario)
                .WithMany(u => u.UsuarioUnidades)
                .HasForeignKey(ud => ud.UsuarioId);
            builder.Entity<BrechaUnidad>()
             .HasKey(k => new { k.BrechaId, k.DivisionId });
            builder.Entity<BrechaUnidad>()
               .HasOne(ud => ud.Brecha)
               .WithMany(u => u.BrechaUnidades)
               .HasForeignKey(ud => ud.BrechaId)
               .OnDelete(DeleteBehavior.Restrict); 
            builder.Entity<BrechaUnidad>()
                .HasOne(ud => ud.Division)
                .WithMany(u => u.BrechaUnidades)
                .HasForeignKey(ud => ud.DivisionId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AccionUnidad>()
            .HasKey(k => new { k.AccionId, k.DivisionId });
            builder.Entity<AccionUnidad>()
               .HasOne(ud => ud.Accion)
               .WithMany(u => u.AcionUnidades)
               .HasForeignKey(ud => ud.AccionId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AccionUnidad>()
                .HasOne(ud => ud.Division)
                .WithMany(u => u.AccionUnidades)
                .HasForeignKey(ud => ud.DivisionId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AccionServicio>()
            .HasKey(k => new { k.AccionId, k.ServicioId });
            builder.Entity<AccionServicio>()
               .HasOne(ud => ud.Accion)
               .WithMany(u => u.AccionServicios)
               .HasForeignKey(ud => ud.AccionId)
               .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<AccionServicio>()
                .HasOne(ud => ud.Servicio)
                .WithMany(u => u.AccionServicios)
                .HasForeignKey(ud => ud.ServicioId)
                .OnDelete(DeleteBehavior.Restrict);
            //builder.Entity<Accion>()
            //     .HasOne<Medida>() // Asume que tienes una clase Medida
            //     .WithMany() // Asume que Medida tiene una colección de Accion
            //     .HasForeignKey(a => a.MedidaId)
            //     .OnDelete(DeleteBehavior.Restrict);
            //builder.Entity<Accion>()
            //    .HasOne<Objetivo>() // Asume que tienes una clase Medida
            //    .WithMany() // Asume que Medida tiene una colección de Accion
            //    .HasForeignKey(a => a.ObjetivoId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // Configuración de relaciones para la entidad Tarea para evitar cascadas múltiples
            builder.Entity<Tarea>()
                .HasOne(t => t.DimensionBrecha)
                .WithMany(db => db.Tareas)
                .HasForeignKey(t => t.DimensionBrechaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Tarea>()
                .HasOne(t => t.Accion)
                .WithMany(a => a.Tareas)
                .HasForeignKey(t => t.AccionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
        /// <summary>
        /// All database tables
        /// </summary>
        public DbSet<EstadoValidacion> EstadoValidacion { get; set; }
        public DbSet<Sexo> Sexo { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Permisos> Permisos { get; set; }
        public DbSet<ArchivoAdjunto> ArchivoAdjuntos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Comuna> Comunas { get; set; }
        public DbSet<Division> Divisiones { get; set; }
        public DbSet<Edificio> Edificios { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<EmpresaDistribuidora> EmpresaDistribuidoras { get; set; }
        public DbSet<Energetico> Energeticos { get; set; }
        public DbSet<Entorno> Entornos { get; set; }
        public DbSet<InerciaTermica> InerciaTermicas { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Medidor> Medidores { get; set; }
        public DbSet<ModoOperacion> ModoOperaciones { get; set; }
        public DbSet<NumeroCliente> NumeroClientes { get; set; }
        public DbSet<Piso> Pisos { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<Region> Regiones { get; set; }
        public DbSet<Rol> Rols { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<SubDivision> SubDivisiones { get; set; }
        public DbSet<TipoAgrupamiento> TipoAgrupamientos { get; set; }
        public DbSet<TipoAislacion> TipoAislaciones { get; set; }
        public DbSet<TipoArchivo> TipoArchivos { get; set; }
        public DbSet<TipoEdificio> TipoEdificios { get; set; }
        public DbSet<TipoMaterial> TipoMateriales { get; set; }
        public DbSet<TipoPropiedad> TipoPropiedades { get; set; }
        public DbSet<TipoPuerta> TipoPuertas { get; set; }
        public DbSet<TipoSombreado> TipoSombreados { get; set; }
        public DbSet<TipoTarifa> TipoTarifas { get; set; }
        public DbSet<TipoTecho> TipoTechos { get; set; }
        public DbSet<TipoTecnologia> TipoTecnologias { get; set; }
        public DbSet<TipoUnidad> TipoUnidades { get; set; }
        public DbSet<TipoUso> TipoUsos { get; set; }
        public DbSet<ParametroMedicion> ParametrosMedicion { get; set; }
        public DbSet<UnidadMedida> UnidadesMedida { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioRol> UsuariosRoles { get; set; }
        public DbSet<UsuarioDivision> UsuariosDivisiones { get; set; }
        public DbSet<UsuarioUnidad> UsuariosUnidades { get; set; }
        public DbSet<UsuarioServicio> UsuariosServicios { get; set; }
        public DbSet<UsuarioInstitucion> UsuariosInstituciones { get; set; }
        public DbSet<MedidorDivision> MedidorDivision { get; set; }
        public DbSet<ReporteRol> ReportesRol { get; set; }
        public DbSet<MenuPanel> MenuPanel { get; set; }
        public DbSet<EnergeticoDivision> EnergeticoDivision { get; set; }
        public DbSet<EnergeticoUnidadMedida> EnergeticoUnidadesMedidas { get; set; }
        public DbSet<CompraMedidor> CompraMedidor { get; set; }
        public DbSet<EmpresaDistribuidoraComuna> EmpresasDistribuidoraComunas { get; set; }
        public DbSet<Trazabilidad> Trazabilidades { get; set; }
        public DbSet<NumeroPiso> NumeroPisos { get; set; }
        public DbSet<MedidorInteligente> MedidoresInteligentes { get; set; }
        public DbSet<MedidorInteligenteEdificio> MedidorInteligenteEdificios { get; set; }
        public DbSet<MedidorInteligenteServicio> MedidorInteligenteServicios { get; set; }
        public DbSet<MedidorInteligenteDivision> MedidorInteligenteDivisiones { get; set; }
        public DbSet<Muro> Muros { get; set; }
        public DbSet<Estructura> Estructuras { get; set; }
        public DbSet<Materialidad> Materialidades { get; set; }
        public DbSet<Aislacion> Aislaciones  { get; set; }
        public DbSet<EstructuraAislacion> EstructuraAislaciones { get; set; }
        public DbSet<EstructuraMaterialidad> EstructuraMaterialidades { get; set; }
        public DbSet<EstructuraTipoSombreado> EstructuraTipodeSombreado { get; set; }
        public DbSet<EstructuraPosicionVentana> EstructuraPosicionVentana { get; set; }
        public DbSet<Techo> Techos { get; set; }
        public DbSet<Suelo> Suelos { get; set; }
        public DbSet<Ventana> Ventanas { get; set; }
        public DbSet<Puerta> Puertas { get; set; }
        public DbSet<Cimiento> Cimientos { get; set; }
        public DbSet<PosicionVentana> PosicionVentanas { get; set; }
        public DbSet<ArchivoDp> ArchivosDp { get; set; }

        public DbSet<Certificado> Certificados { get; set; }

        public DbSet<NotasCertificado> NotasCertificados { get; set; }
        public DbSet<Espesor> Espesores { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<UnidadInmueble> UnidadesInmuebles { get; set; }
        public DbSet<UnidadPiso> UnidadesPisos { get; set; }
        public DbSet<UnidadArea> UnidadesAreas { get; set; }
        public DbSet<Direccion> Direcciones { get; set; }
        public DbSet<Ajuste> Ajustes { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<TipoDocumento> TipoDocumentos { get; set; }
        public DbSet<ActaComite> ActasComite { get; set; }
        public DbSet<ReunionComite> ReunionesComite { get; set; }
        public DbSet<ListaIntegrante> ListaIntegrantes { get; set; }
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Politica> Politicas { get; set; }
        public DbSet<DifusionPolitica> DifusionPoliticas { get; set; }
        public DbSet<ProcedimientoPapel> ProcedimientosPapel { get; set; }
        public DbSet<ProcedimientoResiduo> ProcedimientosResiduo { get; set; }
        public DbSet<ProcedimientoResiduoSistema> ProcedimientosResiduoSistema { get; set; }
        public DbSet<ProcedimientoBajaBienes> ProcedimientosBajaBienes { get; set; }
        public DbSet<ProcedimientoCompraSustentable> ProcedimientosCompraSustentable { get; set; }
        public DbSet<Charla> Charlas { get; set; }
        public DbSet<Impresora> Impresoras { get; set; }
        public DbSet<Resma> Resmas { get; set; }
        public DbSet<Agua> Aguas { get; set; }
        public DbSet<Artefacto> Artefactos { get; set; }
        public DbSet<TipoArtefacto> TipoArtefactos { get; set; }
        public DbSet<Residuo> Residuos { get; set; }
        public DbSet<Contenedor> Contenedores { get; set; }
        public DbSet<Integrante> Integrantes { get; set; }
        public DbSet<ListadoColaborador> ListadoColaboradores { get; set; }
        public DbSet<EncuestaColaborador> EncuestaColaboradores { get; set; }
        public DbSet<Viaje> Viajes { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Aeropuerto> Aeropuertos { get; set; }
        public DbSet<EndPoint> EndPoints { get; set; }
        public DbSet<PermisosBackEnd> PermisosBackEnd { get; set; }
        public DbSet<TipoLuminaria> TiposLuminarias { get; set; }
        public DbSet<TipoEquipoCalefaccion> TiposEquiposCalefaccion { get; set; }
        public DbSet<TipoEquipoCalefaccionEnergetico> TipoEquipoCalefaccionEnergetico { get; set; }
        public DbSet<TipoColector> TiposColectores { get; set; }
        public DbSet<DimensionBrecha> DimensionBrechas { get; set; }
        public DbSet<DimensionServicio> DimensionServicios { get; set; }
        public DbSet<Brecha> Brechas { get; set; }
        public DbSet<BrechaUnidad> BrechaUnidades { get; set; }
        public DbSet<Objetivo> Objetivos { get; set; }
        public DbSet<Medida> Medidas { get; set; }
        public DbSet<Accion> Acciones { get; set; }
        public DbSet<AccionUnidad> AccionUnidades { get; set; }
        public DbSet<AccionServicio> AccionServicios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Indicador> Indicadores { get; set; }
        public DbSet<Programa> Programas { get; set; }
        public DbSet<RespladoUserRole> RespladoUserRoles { get; set; }

        public override int SaveChanges()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(
                    e => e.State == EntityState.Added || 
                    e.State == EntityState.Modified || 
                    e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                var entity = entry.Entity as IAuditable;
                if (entity == null) continue;

                var currentDate = DateTime.Now;

                if (entry.State == EntityState.Added)
                {
                    entity.CreatedAt = currentDate;
                    entity.Active = true;
                }

                if (entry.State == EntityState.Modified || entry.State == EntityState.Added)
                {
            
                    entity.UpdatedAt = currentDate;
                    
                   
                    entity.Version = ++entity.Version;
                }

                if (entry.State == EntityState.Deleted)
                {
                    entity.Active = false;
                    entity.UpdatedAt = currentDate;
                    entry.State = EntityState.Modified;
                    
                }
            }
            return base.SaveChanges();
        }
    }
}
