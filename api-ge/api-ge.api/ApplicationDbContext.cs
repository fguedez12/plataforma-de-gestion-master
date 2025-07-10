using api_gestiona.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace api_gestiona
{
    public class ApplicationDbContext : IdentityDbContext<Usuario>
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {
             
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<UsuarioServicio>()
                .HasKey(k => new { k.ServicioId, k.UsuarioId });

            builder.Entity<UsuarioInstitucion>()
               .HasKey(k => new { k.InstitucionId, k.UsuarioId });
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
        }

        public DbSet<Unidad> Unidades { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<TipoUso> TipoUsos { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<Piso> Pisos { get; set; }
        public DbSet<Division> Divisiones { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Sexo> Sexo { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<UsuarioServicio> UsuariosServicios { get; set; }
        public DbSet<TipoPropiedad> TipoPropiedades { get; set; }
        public DbSet<UsuarioInstitucion> UsuariosInstituciones { get; set; }
        public DbSet<Region> Regiones { get; set; }
        public DbSet<Comuna> Comunas { get; set; }
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
        public DbSet<ProcedimientoReutilizacionPapel> ProcedimientoReutilizacionPapeles { get; set; }
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
        public DbSet<Edificio> Edificios { get; set; }
        public DbSet<Ajuste> Ajustes{ get; set; }
        public DbSet<Medidor> Medidores { get; set; }
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Aeropuerto> Aeropuertos { get; set; }
        public DbSet<CapacitadosMP> CapacitadosMP { get; set; }
        public DbSet<PacE3> PacE3 { get; set; }
        public DbSet<GestionCompraSustentable> GestionCompraSustentables { get; set; }
        public DbSet<TipoLuminaria> TiposLuminarias { get; set; }
        public DbSet<TipoEquipoCalefaccion> TiposEquiposCalefaccion { get; set; }
        public DbSet<Energetico> Energeticos { get; set; }
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
        public DbSet<Indicador> Indicadores { get; set; }
        public DbSet<Programa> Programas { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<NumeroCliente> NumeroClientes { get; set; }
        public DbSet<CompraMedidor> CompraMedidor { get; set; }
        public DbSet<InformeDA> InformesDA { get; set; }
        public DbSet<ResolucionApruebaPlan> ResolucionApruebaPlan { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
    }
}
