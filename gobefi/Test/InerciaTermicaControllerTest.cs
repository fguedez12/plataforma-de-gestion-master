using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.InerciaTermicaModels;
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
    public class InerciaTermicaControllerTest
    {
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<IInerciaTermicaService> _mockInerciaService = new Mock<IInerciaTermicaService>();

        public InerciaTermicaControllerTest()
        {
            var listaInercia = new List<InerciaTermicaModel>();
            listaInercia.Add(new InerciaTermicaModel());

            _mockInerciaService.SetupSequence(service => service.GetAllAsync())
               .ReturnsAsync(new List<InerciaTermicaModel>())
               .ReturnsAsync(listaInercia)
               .Throws(new Exception("Test exception"));
        }

        [Fact]
        public async Task GetInerciaTermica()
        {

            //arrange
            var controller = new InerciaTermicaController(_mockInerciaService.Object);

            //act

            var result1 = await controller.GetInercia();
            var result2 = await controller.GetInercia();
            var okResult = result2 as OkObjectResult;
            var result3 = await controller.GetInercia();

            //assert
            Assert.IsType<NotFoundObjectResult>(result1);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<BadRequestObjectResult>(result3);

        }
    }
}
