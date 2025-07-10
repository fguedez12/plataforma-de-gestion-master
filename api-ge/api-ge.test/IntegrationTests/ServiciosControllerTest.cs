using api_gestiona.Controllers.V2;
using api_gestiona.DTOs.Servicios;
using api_gestiona.Entities;
using api_gestiona.Services;
using api_gestiona.Services.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_ge.test.IntegrationTests
{
    [TestClass]
    public class ServiciosControllerTest : BaseTest
    {
        [TestMethod]
        public async Task GetAll_Return0() 
        {
            //Prep
            var mapper = ConfigAutoMapper();
            var dbName = Guid.NewGuid().ToString();
            var context = BuildDataBaseContext(dbName);
            var mockUserManager = BuildUserManager();
            var service = new ServicioService(mapper, context, mockUserManager);
            
            
            var controller = new ServiciosController(mapper, service);
            //Eje
            var response = await controller.GetAll();
            //val
            var servicios = response.Value;
            Assert.AreEqual(0, servicios.Count);
        }

        [TestMethod]
        public async Task GetAll_return1()
        {
            //Prep
            var mapper = ConfigAutoMapper();
            var dbName = Guid.NewGuid().ToString();
            var context = BuildDataBaseContext(dbName);
            var servicio1 = BuildObjService("Servicio1");
            context.Servicios.Add(servicio1);
            await context.SaveChangesAsync();
            var context2 = BuildDataBaseContext(dbName);
            var mockUserManager = BuildUserManager();
            var service = new ServicioService(mapper, context2, mockUserManager);
            
            var controller = new ServiciosController(mapper, service);
            //Eje
            var response = await controller.GetAll();
            //val
            var servicios = response.Value;
            Assert.AreEqual(1, servicios.Count);
        }

        [TestMethod]
        public async Task GetById_NotExist()
        {
            //Prep
            var mapper = ConfigAutoMapper();
            var dbName = Guid.NewGuid().ToString();
            var context = BuildDataBaseContext(dbName);
            var mockUserManager = BuildUserManager();
            var service = new ServicioService(mapper, context, mockUserManager);
            var controller = new ServiciosController(mapper, service);
            long servicioId = 1;
            //Eje
            var response = await controller.GetById(servicioId);
            var nfResult = response as NotFoundObjectResult;
            //val
            Assert.AreEqual(404, nfResult.StatusCode);
            Assert.AreEqual("No existe el recurso solicitado", nfResult.Value);

        }

        [TestMethod]
        public async Task GetById_Exist()
        {
            //Prep
            var mapper = ConfigAutoMapper();
            var dbName = Guid.NewGuid().ToString();
            var context = BuildDataBaseContext(dbName);
            var mockUserManager = BuildUserManager();
            context.Instituciones.Add(new Institucion { Active = true,Nombre="Institucion1" });
            await context.SaveChangesAsync();
            var servicio1 = BuildObjService("Servicio1");
            servicio1.InstitucionId = 1;
            context.Servicios.Add(servicio1);
            await context.SaveChangesAsync();
            var context2 = BuildDataBaseContext(dbName);
            var service = new ServicioService(mapper, context2, mockUserManager);
            var controller = new ServiciosController(mapper, service);
            long servicioId = 1;
            //Eje
            var response = await controller.GetById(servicioId);
            var okResult = response as OkObjectResult;
            //val
            Assert.AreEqual(200, okResult.StatusCode);
            Assert.IsNotNull(okResult);
        }
    }
}
