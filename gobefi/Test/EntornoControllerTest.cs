using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.EntornoModels;
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
    public class EntornoControllerTest
    {
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<IEntornoService> _mockEntornoService = new Mock<IEntornoService>();


        public EntornoControllerTest()
        {
            var listaEntorno = new List<EntornoModel>();
            listaEntorno.Add(new EntornoModel());

            _mockEntornoService.SetupSequence(service => service.GetAllAsync())
               .ReturnsAsync(new List<EntornoModel>())
               .ReturnsAsync(listaEntorno)
               .Throws(new Exception("Test exception"));
        }

        [Fact]
        public async Task GetTipoEntorno()
        {

            //arrange
            var controller = new EntornoController(_mockEntornoService.Object);

            //act

            var result1 = await controller.GetEntorno();
            var result2 = await controller.GetEntorno();
            var okResult = result2 as OkObjectResult;
            var result3 = await controller.GetEntorno();

            //assert
            Assert.IsType<NotFoundObjectResult>(result1);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<BadRequestObjectResult>(result3);

        }
    }
}
