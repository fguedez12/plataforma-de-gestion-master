using GobEfi.Web.Controllers;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.DTOs;
using GobEfi.Web.DTOs.InmuebleDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GobEfi.Test.UnitTest
{
    [TestClass]
    public class InmueblesControllerV2Test : BaseTest
    {
        [TestMethod]
        public async Task ObtenerTodosLosInmuebles()
        {
            //Preparacion
            var dbName = Guid.NewGuid().ToString();
            var context = ContextBuilder(dbName);
            var mapper = AutoMapperConfiguration();

            context.Divisiones.Add(new Division() { Nombre = "Inmueble 1" });
            context.Divisiones.Add(new Division() { Nombre = "Inmueble 2" });
            await context.SaveChangesAsync();

            var context2 = ContextBuilder(dbName);

            //Prueba
            //var controller = new InmueblesV2Controller(context2, mapper);
            //var respuesta = await controller.Get();

            //verificarion
            //var inmuebles = respuesta.Value;
            //Assert.AreEqual(2, inmuebles.Count);

        }

        [TestMethod]
        public async Task ObtenerInmueblesPaginadosV3()
        {
            //Preparacion
            var dbName = Guid.NewGuid().ToString();
            var context = ContextBuilder(dbName);
            var mapper = AutoMapperConfiguration();

            context.Divisiones.Add(new Division() { Nombre = "Inmueble 1", GeVersion = 3, Active = true });
            context.Divisiones.Add(new Division() { Nombre = "Inmueble 2", GeVersion = 3, Active = true });
            context.Divisiones.Add(new Division() { Nombre = "Inmueble 3", GeVersion = 3, Active = true });
            context.Divisiones.Add(new Division() { Nombre = "Inmueble 4",Active = true });
            await context.SaveChangesAsync();

            var context2 = ContextBuilder(dbName);

            //Prueba
            //var controller = new InmueblesV2Controller(context2, mapper);
            //controller.ControllerContext.HttpContext = new DefaultHttpContext();

            //var pagina1 = await controller.GetGEV3(new PaginationDTO() { Page = 1, PerPage = 2 });
            //var inmueblesPag1 = pagina1.Value;
            //Assert.AreEqual(2, inmueblesPag1.Count);

            //controller.ControllerContext.HttpContext = new DefaultHttpContext();

            //var pagina2 = await controller.GetGEV3(new PaginationDTO() { Page = 2, PerPage = 2 });
            //var inmueblesPag2 = pagina2.Value;
            //Assert.AreEqual(1, inmueblesPag2.Count);

            //controller.ControllerContext.HttpContext = new DefaultHttpContext();

            //var pagina3 = await controller.GetGEV3(new PaginationDTO() { Page = 3, PerPage = 2 });
            //var inmueblesPag3 = pagina3.Value;
            //Assert.AreEqual(0, inmueblesPag3.Count);


        }

        [TestMethod]
        public async Task ObtenerInmueblesPaginadosyFiltradosV3()
        {
            //Preparacion
            var dbName = Guid.NewGuid().ToString();
            var context = ContextBuilder(dbName);
            var mapper = AutoMapperConfiguration();

            context.Divisiones.Add(new Division() { Nombre = "Inmueble 1", Direccion = "Direccion 1", RegionId=1, ComunaId = 1, GeVersion = 3, Active = true });
            context.Divisiones.Add(new Division() { Nombre = "Inmueble 2", Direccion = "Direccion 2", RegionId = 2, ComunaId = 2, GeVersion = 3, Active = true });
            context.Divisiones.Add(new Division() { Nombre = "Inmueble 3", Direccion = "Direccion 3", RegionId = 3, ComunaId = 3, GeVersion = 3, Active = true });
            context.Divisiones.Add(new Division() { Nombre = "Inmueble 4", Direccion = "Direccion 4", RegionId = 4, ComunaId = 4, GeVersion = 3,Active = true });
            await context.SaveChangesAsync();

            var context2 = ContextBuilder(dbName);

            //Prueba
            //var controller = new InmueblesV2Controller(context2, mapper);
            //controller.ControllerContext.HttpContext = new DefaultHttpContext();

            //var filtro1 = await controller.Filter(new InmuebleFilterDTO() { Direccion="1"}, new PaginationDTO() { Page = 1, PerPage = 20 });
            //var inmueblesFiltro1 = filtro1.Value;
            //Assert.AreEqual(1, inmueblesFiltro1.Count);

            //controller.ControllerContext.HttpContext = new DefaultHttpContext();

            //var filtro2 = await controller.Filter(new InmuebleFilterDTO() { RegionId = 1 }, new PaginationDTO() { Page = 1, PerPage = 20 });
            //var inmueblesFiltro2 = filtro2.Value;
            //Assert.AreEqual(1, inmueblesFiltro2.Count);

            //controller.ControllerContext.HttpContext = new DefaultHttpContext();

            //var filtro3 = await controller.Filter(new InmuebleFilterDTO() { ComunaId = 1 }, new PaginationDTO() { Page = 1, PerPage = 20 });
            //var inmueblesFiltro3 = filtro3.Value;
            //Assert.AreEqual(1, inmueblesFiltro3.Count);


        }
    }
}
