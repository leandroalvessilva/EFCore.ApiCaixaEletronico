using ApiCaixaEletronico.DTO;
using ApiCaixaEletronico.Controllers;
using ApiCaixaEletronico.DTO.DTO;
using ApiCaixaEletronico.Service.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.Tests.Controllers
{
    [TestClass]
    public class OperacoesBancariasControllerTest
    {
        private OperacoesBancariasController controller;
        private Mock<IOperacoesBancariasService> mockService;
        private TestesUteis testesUteis;

        public OperacoesBancariasControllerTest()
        {
            mockService = new Mock<IOperacoesBancariasService>();
            controller = new OperacoesBancariasController(mockService.Object);
            testesUteis = new TestesUteis();
        }

        [TestMethod]
        public void OperacoesBancariasController_SaldoTest()
        {
            decimal saldo = 1000;

            mockService.Setup(x => x.Saldo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Returns(new Retorno()
                {
                    Codigo = 200,
                    Data = JsonConvert.SerializeObject(saldo),
                    Mensagem = "Consultado efetuada com sucesso"
                });

            IActionResult result = controller.Saldo(testesUteis.Contas().BancoContaCli, testesUteis.Contas().AgenciaContaCli, testesUteis.Contas().NumeroContaCli, testesUteis.Contas().CpfCli);
            var okResult = result as OkObjectResult;

            Retorno contentResult = (Retorno)okResult.Value;

            Assert.AreEqual(contentResult.Codigo, 200);
            Assert.AreEqual(contentResult.Mensagem, "Consultado efetuada com sucesso");
            Assert.IsNotNull(contentResult.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void OperacoesBancariasController_SaldoExceptionTest()
        {
            mockService.Setup(x => x.Saldo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                .Throws(new Exception("Internal server error"));

            IActionResult result = controller.Saldo(testesUteis.Contas().BancoContaCli, testesUteis.Contas().AgenciaContaCli, testesUteis.Contas().NumeroContaCli, testesUteis.Contas().CpfCli);
            var okResult = result as OkObjectResult;

            Retorno contentResult = (Retorno)okResult.Value;

            Assert.AreEqual(contentResult.Codigo, 500);
            Assert.AreEqual(contentResult.Mensagem, "Internal server error");
            Assert.IsNull(contentResult.Data);
        }

        [TestMethod]
        public void OperacoesBancariasController_SacarTest()
        {
            ContaDTO conta = new ContaDTO();
            OperacaoDTO operacao = new OperacaoDTO();
            decimal valorSacar = 1000;

            mockService.Setup(x => x.Sacar(It.IsAny<ContaDTO>(), It.IsAny<bool>(), It.IsAny<decimal>()))
                .Returns(new Retorno()
                {
                    Codigo = 200,
                    Data = JsonConvert.SerializeObject(operacao),
                    Mensagem = "Saque realizado com sucesso."
                });

            IActionResult result = controller.Sacar(conta, false, valorSacar);
            var okResult = result as OkObjectResult;

            Retorno contentResult = (Retorno)okResult.Value;

            Assert.AreEqual(contentResult.Codigo, 200);
            Assert.AreEqual(contentResult.Mensagem, "Saque realizado com sucesso.");
            Assert.IsNotNull(contentResult.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void OperacoesBancariasController_SacarExceptionTest()
        {
            ContaDTO conta = new ContaDTO();
            decimal valorSacar = 1000;

            mockService.Setup(x => x.Sacar(It.IsAny<ContaDTO>(), It.IsAny<bool>(), It.IsAny<decimal>()))
                .Throws(new Exception("Internal server error"));

            IActionResult result = controller.Sacar(conta, false, valorSacar);
            var okResult = result as OkObjectResult;

            Retorno contentResult = (Retorno)okResult.Value;

            Assert.AreEqual(contentResult.Codigo, 500);
            Assert.AreEqual(contentResult.Mensagem, "Internal server error");
            Assert.IsNull(contentResult.Data);
        }

        [TestMethod]
        public void OperacoesBancariasController_DepositarTest()
        {
            ContaDTO conta = new ContaDTO();
            decimal valorDepositar = 1000;

            mockService.Setup(x => x.Depositar(It.IsAny<ContaDTO>(), It.IsAny<bool>(), It.IsAny<decimal>()))
                .Returns(new Retorno()
                {
                    Codigo = 200,
                    Data = JsonConvert.SerializeObject(true),
                    Mensagem = "Depósito realizado com sucesso."
                });

            IActionResult result = controller.Depositar(conta, false, valorDepositar);
            var okResult = result as OkObjectResult;

            Retorno contentResult = (Retorno)okResult.Value;

            Assert.AreEqual(contentResult.Codigo, 200);
            Assert.AreEqual(contentResult.Mensagem, "Depósito realizado com sucesso.");
            Assert.IsNotNull(contentResult.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void OperacoesBancariasController_DepositarExceptionTest()
        {
            ContaDTO conta = new ContaDTO();
            decimal valorDepositar = 1000;

            mockService.Setup(x => x.Depositar(It.IsAny<ContaDTO>(), It.IsAny<bool>(), It.IsAny<decimal>()))
                .Throws(new Exception("Internal server error"));

            IActionResult result = controller.Depositar(conta, false, valorDepositar);
            var okResult = result as OkObjectResult;

            Retorno contentResult = (Retorno)okResult.Value;

            Assert.AreEqual(contentResult.Codigo, 500);
            Assert.AreEqual(contentResult.Mensagem, "Internal server error");
            Assert.IsNull(contentResult.Data);
        }

        [TestMethod]
        public void OperacoesBancariasController_TransferirTest()
        {
            ContasTransferenciaDTO contasTransferencia = new ContasTransferenciaDTO();
            decimal valorTransferir = 1000;

            mockService.Setup(x => x.Transferir(It.IsAny<ContasTransferenciaDTO>(), It.IsAny<decimal>()))
                .Returns(new Retorno()
                {
                    Codigo = 200,
                    Data = JsonConvert.SerializeObject(true),
                    Mensagem = "Depósito realizado com sucesso."
                });

            IActionResult result = controller.Transferir(contasTransferencia, valorTransferir);
            var okResult = result as OkObjectResult;

            Retorno contentResult = (Retorno)okResult.Value;

            Assert.AreEqual(contentResult.Codigo, 200);
            Assert.AreEqual(contentResult.Mensagem, "Depósito realizado com sucesso.");
            Assert.IsNotNull(contentResult.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void OperacoesBancariasController_TransferirExceptionTest()
        {
            ContasTransferenciaDTO contasTransferencia = new ContasTransferenciaDTO();
            decimal valorTransferir = 1000;

            mockService.Setup(x => x.Transferir(It.IsAny<ContasTransferenciaDTO>(), It.IsAny<decimal>()))
                .Throws(new Exception("Internal server error"));

            IActionResult result = controller.Transferir(contasTransferencia, valorTransferir);
            var okResult = result as OkObjectResult;

            Retorno contentResult = (Retorno)okResult.Value;

            Assert.AreEqual(contentResult.Codigo, 500);
            Assert.AreEqual(contentResult.Mensagem, "Internal server error");
            Assert.IsNull(contentResult.Data);
        }
    }
}
