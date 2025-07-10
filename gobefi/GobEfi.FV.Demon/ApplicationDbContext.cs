using GobEfi.FV.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GobEfi.FV.Demon
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<ModeloEm> Modelos { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=10.0.0.70;database=flota_vehicular;user id=usr_flota;password=iJ8DLleN;Connection Timeout=36000");
        }
    }
}
