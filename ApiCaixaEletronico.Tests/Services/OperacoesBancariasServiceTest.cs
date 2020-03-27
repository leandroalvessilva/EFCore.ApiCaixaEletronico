using ApiCaixaEletronico.DAO;
using ApiCaixaEletronico.DAO.DAO;
using ApiCaixaEletronico.DAO.Interface;
using ApiCaixaEletronico.DTO.DTO;
using ApiCaixaEletronico.Service.Interface;
using ApiCaixaEletronico.Service.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.Tests.Services
{
    [TestClass]
    public class OperacoesBancariasServiceTest
    {
        private OperacoesBancariasService service;
        private Mock<IOperacoesBancariasDAO> mockOperacoesDao;
        private Mock<ICaixaEletronicoDAO> mockCaixaEletronicoDao;
        private TestesUteis testesUteis;

        [TestInitialize]
        public void Initialize()
        {
            mockOperacoesDao = new Mock<IOperacoesBancariasDAO>();
            mockCaixaEletronicoDao = new Mock<ICaixaEletronicoDAO>();
            service = new OperacoesBancariasService(mockOperacoesDao.Object);
            testesUteis = new TestesUteis();
        }

        [TestMethod]
        public void DeveTestar_Saldo()
        {
            var options = testesUteis.CriarDataBaseTeste("SaldoTeste");
            ContaDTO conta = new ContaDTO();

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());

                context.SaveChanges();

                var chamada = service.Saldo(testesUteis.Contas().BancoContaCli, testesUteis.Contas().AgenciaContaCli, testesUteis.Contas().NumeroContaCli, testesUteis.Contas().CpfCli);

                Assert.IsNotNull(chamada);
            }
        }

        [TestMethod]
        public void DeveTestar_Saldo_Exception()
        {
            ContaDTO conta = new ContaDTO();

            mockOperacoesDao.Setup(x => x.Saldo(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<long>()))
                  .Throws(new Exception("erro", new Exception("innter erro")));
            service.Saldo(testesUteis.Contas().BancoContaCli, testesUteis.Contas().AgenciaContaCli, testesUteis.Contas().NumeroContaCli, testesUteis.Contas().CpfCli);
        }

        [TestMethod]
        public void DeveTestar_Sacar()
        {
            var options = testesUteis.CriarDataBaseTeste("SacarTeste");
            
            ContaDTO conta = new ContaDTO();

            decimal valorSacar = 200;

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());
                context.Caixas.Add(testesUteis.ListarCaixas());

                context.SaveChanges();

                var chamada = service.Sacar(conta,false, valorSacar);

                Assert.IsNotNull(chamada);
            }
        }

        [TestMethod]
        public void DeveTestar_Sacar_Exception()
        {
            ContaDTO conta = new ContaDTO();
            decimal valorSacar = 200;

            mockOperacoesDao.Setup(x => x.Sacar(It.IsAny<ContaDTO>(), It.IsAny<bool>(), It.IsAny<decimal>()))
                  .Throws(new Exception("erro", new Exception("innter erro")));
            service.Sacar(conta,false, valorSacar);
        }

        [TestMethod]
        public void DeveTestar_Depositar()
        {
            var options = testesUteis.CriarDataBaseTeste("DepositarTeste");

            ContaDTO conta = new ContaDTO();

            decimal valorSacar = 200;

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());

                context.SaveChanges();

                var chamada = service.Depositar(conta, false, valorSacar);

                Assert.IsNotNull(chamada);
            }
        }

        [TestMethod]
        public void DeveTestar_Depositar_Exception()
        {
            ContaDTO conta = new ContaDTO();
            decimal valorDepositar = 200;

            mockOperacoesDao.Setup(x => x.Depositar(It.IsAny<ContaDTO>(), It.IsAny<bool>(), It.IsAny<decimal>()))
                  .Throws(new Exception("erro", new Exception("innter erro")));
            service.Depositar(conta, false, valorDepositar);
        }

        [TestMethod]
        public void DeveTestar_Transferir()
        {
            var options = testesUteis.CriarDataBaseTeste("TransferirTeste");

            ContasTransferenciaDTO contasTransferir = new ContasTransferenciaDTO();

            decimal valorSacar = 200;

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());

                context.SaveChanges();

                var chamada = service.Transferir(contasTransferir, valorSacar);

                Assert.IsNotNull(chamada);
            }
        }

        [TestMethod]
        public void DeveTestar_Transferir_Exception()
        {
            ContasTransferenciaDTO contasTransferir = new ContasTransferenciaDTO();

            decimal valorDepositar = 200;

            mockOperacoesDao.Setup(x => x.Transferir(It.IsAny<ContasTransferenciaDTO>(), It.IsAny<decimal>()))
                  .Throws(new Exception("erro", new Exception("innter erro")));
            service.Transferir(contasTransferir, valorDepositar);
        }
    }
}
