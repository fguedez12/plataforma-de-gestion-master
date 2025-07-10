using GobEfi.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GobEfi.Web.Core.Contracts;
using Microsoft.AspNetCore.Identity;
using GobEfi.Web.Data.Entities;
using static GobEfi.Web.Core.Constants;

namespace GobEfi.Web.Data
{
    public class InitialData
    {
        public static async Task Seed(
            ApplicationDbContext ctx,
            UserManager<Usuario> userManager,
            RoleManager<Rol> roleManager)
        {
            ctx.Database.EnsureCreated();

            if (!ctx.Rols.Any())
            {
                await roleManager.CreateAsync(new Rol {
                    Name = Claims.ES_ADMINISTRADOR,
                    Nombre = "Administrador" });
                await roleManager.CreateAsync(new Rol {
                    Name = Claims.ES_DESARROLLADOR,
                    Nombre = "Desarrollador" });
                await roleManager.CreateAsync(new Rol {
                    Name = Claims.ES_GESTORSERVICIO,
                    Nombre = "Gestor de Servicio" });
                await roleManager.CreateAsync(new Rol {
                    Name = Claims.ES_GESTORUNIDAD,
                    Nombre = "Gestor de Unidad" });
                await roleManager.CreateAsync(new Rol {
                    Name = Claims.ES_GESTORCONSULTA,
                    Nombre = "Gestor de Consulta" });
                await roleManager.CreateAsync(new Rol {
                    Name = Claims.ES_AUDITOR,
                    Nombre = "Auditor" });
                await roleManager.CreateAsync(new Rol {
                    Name = Claims.ES_USUARIO,
                    Nombre = "Usuario" });
            }

            if (!ctx.Usuarios.Any())
            {
                var result = await userManager.CreateAsync(
                    new Usuario
                    {
                        Nombres = "Cristian",
                        Apellidos = "Recabarren",
                        UserName = "crecabarren@minenergia.cl",
                        Email = "crecabarren@minenergia.cl",
                        EmailConfirmed = true
                    },
                    "New2me!!"
                );
                var usuario = await userManager.FindByNameAsync("crecabarren@minenergia.cl");
                await userManager.AddToRoleAsync(usuario, Claims.ES_ADMINISTRADOR);
                await userManager.AddToRoleAsync(usuario, Claims.ES_DESARROLLADOR);
                await userManager.AddToRoleAsync(usuario, Claims.ES_USUARIO);
            }
        }
    }
}
