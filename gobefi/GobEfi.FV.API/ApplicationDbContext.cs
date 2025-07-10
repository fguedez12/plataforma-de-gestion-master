using GobEfi.FV.API.Models.Entities;
using GobEfi.FV.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.API
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) :base(options)
        {

        }

        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<ModeloEm> Modelos { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Propulsion> Propulsiones { get; set; }
        public DbSet<Combustible> Combustibles { get; set; }
    }
}
