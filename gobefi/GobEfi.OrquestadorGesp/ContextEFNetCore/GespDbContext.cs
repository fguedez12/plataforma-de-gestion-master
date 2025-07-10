using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using OrquestadorGesp.AppSettingsJson;
using System.Linq;

namespace OrquestadorGesp.ContextEFNetCore
{
	public class GespDbContext : DbContext
	{
		public DbSet<_BasePkServicioEntity> ServicioInfoBaseSet { get; set; }
		public DbSet<UnidadesPorCadaServicioEnt> UnidadesPorServicioSet { get; set; }
		public DbSet<UnidadesPorServicioConEficEnerg> UnidadesPorServicioConEficEnergSet { get; set; }
		public DbSet<ComprasReporteExtendido> ComprasReporteExtendidoSet { get; set; }
		public DbSet<ReporteCompactoEncabezadoEnt> ReporteCompactoEncabezadoSet { get; set; }

		public DbSet<ReporteControlDeCargaEnt> ReporteControlDeCargaSet { get; set; }
		public static GespDbContext GetInstance()
		{
			return new GespDbContext();
		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			foreach (var entityType in modelBuilder.Model.GetEntityTypes()
			.Where(t => t.ClrType.IsSubclassOf(typeof(_BasePkEntity))))
			{
				modelBuilder.Entity(
						entityType.Name,
						x =>
						{
							x.HasKey("Int64PK");
						});
			}
			modelBuilder.Entity<ComprasReporteExtendido>()
				.HasKey(c => new { c.IdDivisionCompra, c.IdCompra, c.IdCompraMedidor});
			foreach ( var prop in modelBuilder.Entity<ComprasReporteExtendido>().Metadata.GetProperties())
			{
				if (prop.ClrType == typeof(float))
				{
					prop.Relational().DefaultValue = -1.0f;
				}
				if (prop.ClrType == typeof(long) || prop.ClrType == typeof(long?))
				{
					prop.Relational().DefaultValue = -1L;
				}
			}
			modelBuilder.Entity<ReporteCompactoEncabezadoEnt>()
				.HasKey(r => new { r.DivisionId, r.EnergeticoId });
				
			modelBuilder.Entity<ReporteControlDeCargaEnt>()
				.HasKey(r => new { r.DivisionId, r.EnergeticoId, r.NroClienteId, r.MedidorDivisionId });

					//modelBuilder.Entity<ComprasReporteExtendido>()
						//	.Property(p => p.IdDeMedidor)
						//	.HasDefaultValueSql("-1")
						//	.HasDefaultValue(-1);
						//modelBuilder.Entity<ComprasReporteExtendido>()
						//	.Property(p => p.IdDivisionDelMedidor)
						//	.HasDefaultValueSql("-1")
						//	.HasDefaultValue(-1);
						//modelBuilder.Entity<ComprasReporteExtendido>()
						//	.Property(p => p.IdEdificioDelMedidor)
						//	.HasDefaultValueSql("-1")
						//	.HasDefaultValue(-1);
						//modelBuilder.Entity<ComprasReporteExtendido>()
						//	.Property(p => p.IdNumeroDeCliente)
						//	.HasDefaultValueSql("-1")
						//	.HasDefaultValue(-1);
		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (optionsBuilder.IsConfigured)
			{
				return;
			}

			optionsBuilder.UseSqlServer(CurrentConfJson.CurrentConf.DBConnValue);
			
		}
	}
}
