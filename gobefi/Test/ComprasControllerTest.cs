using AutoMapper;
using GobEfi.Web.Controllers;
using GobEfi.Web.Core.Contracts.Services;
using GobEfi.Web.Data.Entities;
using GobEfi.Web.Models.CompraModels;
using GobEfi.Web.Models.EnergeticoModels;
using GobEfi.Web.Services;
using GobEfi.Web.Services.Request;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GobEfi.Test
{
    public class ComprasControllerTest
    {
        private readonly Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private readonly Mock<ICompraService> _mockComprasService = new Mock<ICompraService>();
        private readonly Mock<ILoggerFactory> _mockLogger = new Mock<ILoggerFactory>();
        private readonly Mock<IUsuarioService> _mockUsuarioService = new Mock<IUsuarioService>();
        private readonly Mock<IEnergeticoService> _mockEnergeticoService = new Mock<IEnergeticoService>();
        private readonly Mock<IEmailSender> _mockEmailSender= new Mock<IEmailSender>();
        private readonly Mock<IHttpContextAccessor> _mockContextAccessor = new Mock<IHttpContextAccessor>();

        public ComprasControllerTest() {

            
            _mockComprasService
                .Setup(service => service.GetParaValidar(It.IsAny<long>()))
                .ReturnsAsync( (long param)=>
                    {
                        if (param == 0)
                        {
                            return null;
                          
                        }
                        if (param == 1)
                        {
                            return new CompraParaValidarDetalleModel() { Id = 1, Consumo = 56 };
                        }
                        else
                        {
                            throw new Exception("Test exception");
                        }
                       
                    }
                    
                );
            _mockComprasService
                 .Setup(service => service.ValidaPermiso(It.IsAny<long>()))
                 .ReturnsAsync((long param) => {
                     if (param == 0)
                     {
                         return false;
                     }

                     if (param == 2)
                     {
                         throw new Exception("Test Exception");
                     }
                     else {

                         return true;
                     }
                    
                 });

            _mockComprasService
                 .Setup(service => service.getComprasParaValidar(It.IsAny<CompraParaValidarRequest>()))
                 .ReturnsAsync((CompraParaValidarRequest param) => {
                     if (param.InstitucionId == 0)
                     {
                         throw new Exception("Test Exception");
                     }
                     else {
                         return new List<CompraParaValidarModel>();

                     }
                 });

            _mockComprasService
            .Setup(s => s.ObtenerLasCompras(It.IsAny<long>(), It.IsAny<List<EnergeticoActivoModel>>()))
            .Returns(new List<CompraTablaEnergetico>());

            _mockComprasService
           .Setup(s => s.AccionEstado(It.IsAny<long>(), It.IsAny<string>(), It.IsAny<string>()))
           .ReturnsAsync( (long paramId, string paramAccion)=> {

               if (paramId == 0)
               {

                   throw new Exception("Test Exception");
               }
               else
               {
                   return "OK";
               }

           } );

            _mockComprasService
            .Setup(s => s.Update(It.IsAny<long>(), It.IsAny<CompraForEdit>()))
            .ReturnsAsync((long idParam, CompraForEdit requestParam) => {

                if (idParam != requestParam.Id)
                {

                    throw new Exception("Test Exception");
                }
                else
                {
                    return 1;
                }

            });

            _mockComprasService
           .Setup(s => s.Add(It.IsAny<CompraForRegister>()))
           .ReturnsAsync((CompraForRegister requestParam) => {

               if (requestParam.Id == 1)
               {
                   throw new Exception("Test Exception");
               }
               else {

                   return 1;
               }

           });

            _mockComprasService
            .Setup(service => service.Get(It.IsAny<long>()))
            .Returns((long param) =>
            {
                if (param == 0)
                {
                    return null;
                }
                else
                {

                    return new CompraModel() { Id = 1 };
                }

            });

            _mockEnergeticoService
                .Setup(service => service.GetActivosByDivision(It.IsAny<long>()))
                .ReturnsAsync((long param) =>
                {
                    var list = new List<EnergeticoActivoModel>();
                    list.Add(new EnergeticoActivoModel() { Id = 3, Nombre = "Electricidad", TieneNumCliente = true });

                    return list;
                });

            
        }

        //[Fact]
        //public async Task GetComprasParaValidarTest() {

        //    //arrange
        //    var controller = new ComprasController(_mockMapper.Object, _mockComprasService.Object, _mockLogger.Object, _mockUsuarioService.Object,_mockEmailSender.Object, 
        //        _mockEnergeticoService.Object);


        //    //act
        //    var result1 = await controller.GetComprasParaValidar(1);
        //    var okResult = result1 as OkObjectResult;
        //    var result2 = await controller.GetComprasParaValidar(0);
        //    var result3 = await controller.GetComprasParaValidar(3);

        //    //assert
        //    Assert.NotNull(okResult);
        //    Assert.Equal(200, okResult.StatusCode);
        //    Assert.IsType<NotFoundResult>(result2);
        //    Assert.IsType<BadRequestObjectResult>(result3);
        //}

        //[Fact]
        //public async Task GetComprasByDivisionTest() {
        //    //arrange

        //    var controller = new ComprasController(_mockMapper.Object, _mockComprasService.Object, _mockLogger.Object, _mockUsuarioService.Object, _mockEmailSender.Object,
        //        _mockEnergeticoService.Object);
        //    //act

        //    var result1 = await controller.GetComprasByDivision(0);
        //    var nokResul = result1 as UnauthorizedResult;
        //    var result2 = await controller.GetComprasByDivision(1);
        //    var okResult = result2 as OkObjectResult;
        //    var result3 = await controller.GetComprasByDivision(2);

        //    //assert

        //    Assert.Equal(401, nokResul.StatusCode);
        //    Assert.NotNull(okResult);
        //    Assert.Equal(200, okResult.StatusCode);
        //    Assert.IsType<BadRequestObjectResult>(result3);

        //}

        //[Fact]
        //public async Task GetComprasParaValidar2Test()
        //{
        //    //arrange

        //    var controller = new ComprasController(_mockMapper.Object, _mockComprasService.Object, _mockLogger.Object, _mockUsuarioService.Object, _mockEmailSender.Object,
        //        _mockEnergeticoService.Object);
        //    var requestOk = new CompraParaValidarRequest() { InstitucionId = 1 };
        //    var requestNok = new CompraParaValidarRequest() { InstitucionId = 0 };
        //    //act

        //    var resultNok = await controller.GetComprasParaValidar(requestNok);
        //    var resultOk = await controller.GetComprasParaValidar(requestOk);
        //    var okResult = resultOk as OkObjectResult;

        //    //assert

        //    Assert.IsType<BadRequestObjectResult>(resultNok);
        //    Assert.NotNull(okResult);
        //    Assert.Equal(200, okResult.StatusCode);

        //}
        //[Fact]
        //public async Task PutCompraTest() {

        //    //arrange

        //    var controller = new ComprasController(_mockMapper.Object, _mockComprasService.Object, _mockLogger.Object, _mockUsuarioService.Object, _mockEmailSender.Object,
        //        _mockEnergeticoService.Object);
        //    var request = new CompraForEdit() { Id=1 };

        //    //act
        //    var resultNok = await controller.PutCompra(2, request);
        //    var resultOk = await controller.PutCompra(1, request);
        //    var okResult = resultOk as NoContentResult;

        //    //assert
        //    Assert.IsType<BadRequestObjectResult>(resultNok);
        //    Assert.NotNull(resultOk);
        //    Assert.Equal(204, okResult.StatusCode);

        //}

        //[Fact]
        //public async Task PostAccionEstadoTest()
        //{
        //    //arrange
        //    var controller = new ComprasController(_mockMapper.Object, _mockComprasService.Object, _mockLogger.Object, _mockUsuarioService.Object, _mockEmailSender.Object,
        //        _mockEnergeticoService.Object);

        //    //act
        //    var resultNok = await controller.PostAccionEstado(0, "Test1","obs 1");
        //    var resultOk = await controller.PostAccionEstado(1, "Test1","obs 1");
        //    var okResult = resultOk as OkObjectResult;

        //    //assert
        //    Assert.IsType<BadRequestObjectResult>(resultNok);
        //    Assert.NotNull(resultOk);
        //    Assert.Equal(200, okResult.StatusCode);
        //}

        

        //[Fact]
        //public async Task PostCompraTest()
        //{
        //    //arrange
        //    var controller = new ComprasController(_mockMapper.Object, _mockComprasService.Object, _mockLogger.Object, _mockUsuarioService.Object, _mockEmailSender.Object,
        //        _mockEnergeticoService.Object);

        //    var nokCompra = new CompraForRegister() { Id=1};
        //    var okCompra = new CompraForRegister() { Id = 0 };

        //    //act
        //    var resultNok = await controller.PostCompra(nokCompra);
        //    var resultOk = await controller.PostCompra(okCompra);
        //    var okResult = resultOk as CreatedAtActionResult;

        //    //assert
        //    Assert.IsType<BadRequestObjectResult>(resultNok);
        //    Assert.NotNull(resultOk);
        //    Assert.Equal(201, okResult.StatusCode);
        //}


        //[Fact]
        //public async Task DeleteCompraTest()
        //{
        //    //arrange
        //    var controller = new ComprasController(_mockMapper.Object, _mockComprasService.Object, _mockLogger.Object, _mockUsuarioService.Object, _mockEmailSender.Object,
        //        _mockEnergeticoService.Object);

        //    //act
        //    var resultNok = await controller.DeleteCompra(0);
        //    var resultOk = await controller.DeleteCompra(1);
        //    var okResult = resultOk as OkObjectResult;

        //    //assert
        //    Assert.IsType<NotFoundResult>(resultNok);
        //    Assert.NotNull(resultOk);
        //    Assert.Equal(200, okResult.StatusCode);
        //}

    }
}
