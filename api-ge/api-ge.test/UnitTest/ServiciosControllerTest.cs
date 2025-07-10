using api_gestiona.Controllers.V2;
using api_gestiona.DTOs.Servicios;
using api_gestiona.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Tavis.UriTemplates;

namespace api_ge.test.UnitTest
{
    [TestClass]
    public class ServiciosControllerTest : BaseTest
    {
        [TestMethod]
        public async Task GetAll_return0()
        {
            //Prep
            var mapper = ConfigAutoMapper();
            var mockServicioService = new Mock<IServicioService>();
            var list = new List<ServicioListDTO>();
            mockServicioService.Setup(x => x.GetAll()).Returns(Task.FromResult(list));
            var controller = new ServiciosController(mapper,mockServicioService.Object);
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
            var mockServicioService = new Mock<IServicioService>();
            var serciceDTO = new ServicioListDTO { Id = 1,Nombre = "Servicio 1" };
            var list = new List<ServicioListDTO>();
            list.Add(serciceDTO);
            mockServicioService.Setup(x => x.GetAll()).Returns(Task.FromResult(list));
            var controller = new ServiciosController(mapper, mockServicioService.Object);
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
            var mockServicioService = new Mock<IServicioService>();
            mockServicioService.Setup(x => x.Exist(It.IsAny<long>())).Returns(Task.FromResult(false));
            var controller = new ServiciosController(mapper, mockServicioService.Object);
            long servicioId = 1;
            //Eje
            var response = await controller.GetById(servicioId);
            var nfResult = response as NotFoundObjectResult;
            //val
            Assert.AreEqual(404, nfResult.StatusCode);
            Assert.AreEqual("No existe el recurso solicitado",nfResult.Value);

        }

        [TestMethod]
        public async Task GetById_Exist()
        {
            //Prep
            var mapper = ConfigAutoMapper();
            var mockServicioService = new Mock<IServicioService>();
            var servicioListDto = new ServicioListDTO { Id = 1, Nombre = "Servicio 1" };
            mockServicioService.Setup(x => x.Exist(It.IsAny<long>())).Returns(Task.FromResult(true));
            mockServicioService.Setup(x => x.GetById(It.IsAny<long>())).Returns(Task.FromResult(servicioListDto));

            var controller = new ServiciosController(mapper, mockServicioService.Object);
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
