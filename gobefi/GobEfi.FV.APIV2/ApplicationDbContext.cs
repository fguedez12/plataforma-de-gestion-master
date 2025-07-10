using GobEfi.FV.APIV2.Models.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GobEfi.FV.APIV2
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<ModeloEm> Modelos { get; set; }
        public DbSet<Imagen> Imagenes { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
    }
}
