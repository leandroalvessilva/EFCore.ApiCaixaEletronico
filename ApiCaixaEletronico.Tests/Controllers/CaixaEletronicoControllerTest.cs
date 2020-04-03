using ApiCaixaEletronico.Controllers;
using ApiCaixaEletronico.DTO;
using ApiCaixaEletronico.DTO.DTO;
using ApiCaixaEletronico.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;

namespace ApiCaixaEletronico.Tests.Controllers
{
    [TestClass]
    public class CaixaEletronicoControllerTest
    {
        private CaixaEletronicoController controller;
        private Mock<ICaixaEletronicoService> mockService;
        private TestesUteis testesUteis;

        public CaixaEletronicoControllerTest()
        {
            mockService = new Mock<ICaixaEletronicoService>();
            controller = new CaixaEletronicoController(mockService.Object);
            testesUteis = new TestesUteis();
        }

        [TestMethod]
        public void OperacoesBancariasController_LoginTest()
        {
            mockService.Setup(x => x.Login(It.IsAny<long>(), It.IsAny<int>()))
                .Returns(new Retorno()
                {
                    Codigo = 200,
                    Data = JsonConvert.SerializeObject(true),
                    Mensagem = "Login efetuado com sucesso."
                });

            IActionResult result = controller.Login(testesUteis.Contas().CpfCli, testesUteis.ListarContas().SenhaConta);

            var okResult = result as OkObjectResult;

            Retorno contentResult = (Retorno)okResult.Value;

            Assert.AreEqual(contentResult.Codigo, 200);
            Assert.AreEqual(contentResult.Mensagem, "Login efetuado com sucesso.");
            Assert.IsNotNull(contentResult.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void OperacoesBancariasController_LoginExceptionTest()
        {
            mockService.Setup(x => x.Login(It.IsAny<long>(), It.IsAny<int>()))
                .Throws(new Exception("Internal server error"));

            IActionResult result = controller.Login(testesUteis.Contas().CpfCli, testesUteis.ListarContas().SenhaConta);

            var okResult = result as OkObjectResult;

            Retorno contentResult = (Retorno)okResult.Value;
        }

        [TestMethod]
        public void OperacoesBancariasController_ListarUsuarioTest()
        {
            ContaDTO conta = new ContaDTO();

            mockService.Setup(x => x.ListarUsuario(It.IsAny<long>()))
                .Returns(new Retorno()
                {
                    Codigo = 200,
                    Data = JsonConvert.SerializeObject(conta),
                    Mensagem = "Consulta efetuada com sucesso."
                });

            IActionResult result = controller.ListarUsuario(testesUteis.Contas().CpfCli);

            var okResult = result as OkObjectResult;

            Retorno contentResult = (Retorno)okResult.Value;

            Assert.AreEqual(contentResult.Codigo, 200);
            Assert.AreEqual(contentResult.Mensagem, "Consulta efetuada com sucesso.");
            Assert.IsNotNull(contentResult.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void OperacoesBancariasController_ListarUsuarioExceptionTest()
        {
            mockService.Setup(x => x.ListarUsuario(It.IsAny<long>()))
                .Throws(new Exception("Internal server error"));

            IActionResult result = controller.ListarUsuario(testesUteis.Contas().CpfCli);

            var okResult = result as OkObjectResult;

            Retorno contentResult = (Retorno)okResult.Value;
        }
    }
}
