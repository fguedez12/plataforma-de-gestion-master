using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.TipoAgrupamientoModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Web.Controllers;
using Xunit;

namespace GobEfi.Test
{
    public class TipoAgrupamientoControllerTest
    {
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ITipoAgrupamientoService> _mockTipoAgrupamientoService = new Mock<ITipoAgrupamientoService>();
        public TipoAgrupamientoControllerTest() {

            var listaAgrupamiento = new List<TipoAgrupamientoModel>();
            listaAgrupamiento.Add(new TipoAgrupamientoModel());


            _mockTipoAgrupamientoService.SetupSequence(service => service.GetAllAsync())
                .ReturnsAsync(new List<TipoAgrupamientoModel>())
                .ReturnsAsync(listaAgrupamiento)
                .Throws(new Exception("Test exception"));

        }

        [Fact]
        public async Task GetTipoAgrupamiento() {

            //arrange
            var controller = new TipoAgrupamientoController(_mockTipoAgrupamientoService.Object);

            //act

            var result1 = await controller.GetTipoAgrupamiento();
            var result2 = await controller.GetTipoAgrupamiento();
            var okResult = result2 as OkObjectResult;
            var result3 = await controller.GetTipoAgrupamiento();

            //assert
            Assert.IsType<NotFoundObjectResult>(result1);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<BadRequestObjectResult>(result3);

        }
    }
}
