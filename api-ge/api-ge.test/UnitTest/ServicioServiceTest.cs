using api_gestiona.Entities;
using api_gestiona.Services;
using Bogus;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_ge.test.UnitTest
{
    [TestClass]
    public class ServicioServiceTest :BaseTest
    {

        [TestMethod]
        public async Task GetAll_Test_Return0()
        {
            //Prep
            var dbName = Guid.NewGuid().ToString();
            var context = BuildDataBaseContext(dbName);
            var mapper = ConfigAutoMapper();
            var mockUserManager = BuildUserManager();
            var service = new ServicioService(mapper, context, mockUserManager);
            //Eje
            var list = await service.GetAll();
            var count = list.Count();
            //Val
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public async Task GetAll_Test() 
        {
            //Prep
            var dbName = Guid.NewGuid().ToString();
            var context = BuildDataBaseContext(dbName);
            var mapper = ConfigAutoMapper();
            var mockUserManager = BuildUserManager();
            var institucion = new Institucion { Nombre = "Institucion 1",Active =true };
            context.Instituciones.Add(institucion);
            await context.SaveChangesAsync();
            var service1 = BuildObjService("Servicio 1");
            var service2 = BuildObjService("Servicio 2");
            context.Servicios.Add(service1);
            context.Servicios.Add(service2);
            await context.SaveChangesAsync();
            var context2 = BuildDataBaseContext(dbName);
            var service = new ServicioService(mapper, context2, mockUserManager);
            //Eje
            var list = await service.GetAll();
            var count = list.Count();
            //Val
            Assert.AreEqual(2, count);
            
        }

        [TestMethod]
        public async Task Exist_true()
        {
            //Prep
            var dbName = Guid.NewGuid().ToString();
            var context = BuildDataBaseContext(dbName);
            var mapper = ConfigAutoMapper();
            var mockUserManager = BuildUserManager();
            var institucion = new Institucion { Nombre = "Institucion 1", Active = true };
            context.Instituciones.Add(institucion);
            await context.SaveChangesAsync();
            var service1 = BuildObjService("Servicio 1");
            context.Servicios.Add(service1);
            await context.SaveChangesAsync();
            var context2 = BuildDataBaseContext(dbName);
            var service = new ServicioService(mapper, context2, mockUserManager);
            var serviceId = 1;
            //Eje
            var result = await service.Exist(serviceId);
            //Val
            Assert.IsTrue(result);  
        }

        [TestMethod]
        public async Task Exist_false()
        {
            //Prep
            var dbName = Guid.NewGuid().ToString();
            var context = BuildDataBaseContext(dbName);
            var mapper = ConfigAutoMapper();
            var mockUserManager = BuildUserManager();
            var institucion = new Institucion { Nombre = "Institucion 1", Active = true };
            context.Instituciones.Add(institucion);
            await context.SaveChangesAsync();
            var service1 = BuildObjService("Servicio 1");
            context.Servicios.Add(service1);
            await context.SaveChangesAsync();
            var context2 = BuildDataBaseContext(dbName);
            var service = new ServicioService(mapper, context2, mockUserManager);
            var serviceId = 2;
            //Eje
            var result = await service.Exist(serviceId);
            //Val
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task GetById_Return_null()
        {
            //Prep
            var dbName = Guid.NewGuid().ToString();
            var context = BuildDataBaseContext(dbName);
            var mapper = ConfigAutoMapper();
            var mockUserManager = BuildUserManager();
            var service = new ServicioService(mapper, context, mockUserManager);
            //Eje
            int id = 1;
            var result = await service.GetById(id);
            //Val
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetById()
        {
            //Prep
            var dbName = Guid.NewGuid().ToString();
            var context = BuildDataBaseContext(dbName);
            var mapper = ConfigAutoMapper();
            var mockUserManager = BuildUserManager();
            var institucion = new Institucion { Nombre = "Institucion 1", Active = true };
            context.Instituciones.Add(institucion);
            await context.SaveChangesAsync();
            var service1 = BuildObjService("Servicio 1");
            context.Servicios.Add(service1);
            await context.SaveChangesAsync();
            var context2 = BuildDataBaseContext(dbName);
            var service = new ServicioService(mapper, context2, mockUserManager);
            var serviceId = 1;
            //Eje
            var result = await service.GetById(serviceId);
            //Val
            Assert.IsNotNull(result);
        }
    }
}
