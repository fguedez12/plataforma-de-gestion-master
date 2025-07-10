using AutoMapper;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Models.NumeroPisoModels;
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
    public class NumeroPisoControllerTest
    {
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<INumeroPisoService> _mockNumeroPisoService = new Mock<INumeroPisoService>();

        public NumeroPisoControllerTest()
        {
            var listaNUmeroPiso = new List<NumeroPisoModel>();
            listaNUmeroPiso.Add(new NumeroPisoModel());

            _mockNumeroPisoService.SetupSequence(service => service.GetAllAsync())
               .ReturnsAsync(new List<NumeroPisoModel>())
               .ReturnsAsync(listaNUmeroPiso)
               .Throws(new Exception("Test exception"));

        }

        [Fact]
        public async Task GetNumeroPiso()
        {

            //arrange
            var controller = new NumeroPisoController(_mockNumeroPisoService.Object);

            //act

            var result1 = await controller.GetNumeroPiso();
            var result2 = await controller.GetNumeroPiso();
            var okResult = result2 as OkObjectResult;
            var result3 = await controller.GetNumeroPiso();

            //assert
            Assert.IsType<NotFoundObjectResult>(result1);
            Assert.NotNull(okResult);
            Assert.Equal(200, okResult.StatusCode);
            Assert.IsType<BadRequestObjectResult>(result3);

        }

    }
}
