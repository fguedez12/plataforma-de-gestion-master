using GobEfi.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace GobEfi.Services
{
    public class DbContext :IdentityDbContext
    {
       

        public DbContext(DbContextOptions<DbContext> options)
            :base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<MedidorInteligente> MedidoresInteligentes { get; set; }
        public DbSet<MedidorInteligenteEdificio> MedidorInteligenteEdificios { get; set; }
        public DbSet<MedidorInteligenteServicio> MedidorInteligenteServicios { get; set; }
        public DbSet<Institucion> Instituciones { get; set; }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Edificio> Edificios { get; set; }
        public DbSet<TipoEdificio> TipoEdificios { get; set; }
        public DbSet<Comuna> Comunas { get; set; }
        public DbSet<Division> Divisiones { get; set; }
        public DbSet<Region> Regiones { get; set; }
        public DbSet<Provincia> Provincias { get; set; }
        public DbSet<MedidorInteligenteDivision> MedidorInteligenteDivisiones { get; set; }
        public DbSet<Medidor> Medidores { get; set; }
        public DbSet<MedidorDivision> MedidorDivision { get; set; }


    }
}
