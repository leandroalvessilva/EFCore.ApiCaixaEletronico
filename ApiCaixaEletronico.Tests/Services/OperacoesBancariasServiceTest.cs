using ApiCaixaEletronico.DAO;
using ApiCaixaEletronico.DAO.DAO;
using ApiCaixaEletronico.DAO.Interface;
using ApiCaixaEletronico.DTO.DTO;
using ApiCaixaEletronico.Service.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

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

                Assert.AreEqual(200,chamada.Codigo);
                Assert.IsNotNull(chamada);
                Assert.AreEqual("Consulta efetuada com sucesso", chamada.Mensagem);
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

                mockOperacoesDao.Setup(x => x.Sacar(It.IsAny<ContaDTO>(), It.IsAny<decimal>())).Returns(new OperacaoDTO() {Realizada = true });

                var chamada = service.Sacar(conta, valorSacar);

                Assert.AreEqual(200,chamada.Codigo);
                Assert.IsNotNull(chamada.Data);
                Assert.AreEqual("Saque realizado com sucesso.", chamada.Mensagem);
            }
        }

        [TestMethod]
        public void DeveTestar_SacarC2()
        {
            var options = testesUteis.CriarDataBaseTeste("SacarC2Teste");

            ContaDTO conta = new ContaDTO();

            decimal valorSacar = 200;

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());
                context.Caixas.Add(testesUteis.ListarCaixas());

                context.SaveChanges();

                mockOperacoesDao.Setup(x => x.Sacar(It.IsAny<ContaDTO>(), It.IsAny<decimal>())).Returns(new OperacaoDTO() { Realizada = false });

                var chamada = service.Sacar(conta, valorSacar);

                Assert.AreEqual(500, chamada.Codigo);
                Assert.AreEqual("Não há notas disponíveis para realizar este saque.", chamada.Mensagem);
            }
        }

        [TestMethod]
        public void DeveTestar_Sacar_Exception()
        {
            ContaDTO conta = new ContaDTO();
            decimal valorSacar = 200;

            mockOperacoesDao.Setup(x => x.Sacar(It.IsAny<ContaDTO>(), It.IsAny<decimal>()))
                  .Throws(new Exception("erro", new Exception("innter erro")));
            service.Sacar(conta, valorSacar);
        }

        [TestMethod]
        public void DeveTestar_Depositar()
        {
            var options = testesUteis.CriarDataBaseTeste("DepositarTeste");

            ContaDTO conta = new ContaDTO();

            decimal valorSacar = 200;

            string notasUtilizada = "1,1,1,1";


            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());

                context.SaveChanges();

                var chamada = service.Depositar(conta, valorSacar, notasUtilizada);

                Assert.AreEqual(500, chamada.Codigo);
                Assert.AreEqual("O valor para depósito não pode ser zero e deve ser menor que o limite definido pelo banco de R$5.000,00.", chamada.Mensagem);
            }
        }

        [TestMethod]
        public void DeveTestar_DepositarC1()
        {
            var options = testesUteis.CriarDataBaseTeste("DepositarC1Teste");

            ContaDTO conta = new ContaDTO();

            decimal valorSacar = 200;

            string notasUtilizada = "1,1,1,1";


            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());

                context.SaveChanges();

                mockOperacoesDao.Setup(x => x.Depositar(It.IsAny<ContaDTO>(), It.IsAny<decimal>(), It.IsAny<string>())).Returns(true);
                var chamada = service.Depositar(conta, valorSacar, notasUtilizada);

                Assert.AreEqual(200, chamada.Codigo);
                Assert.IsNotNull(chamada.Codigo);
                Assert.AreEqual("Depósito realizado com sucesso.", chamada.Mensagem);
            }
        }

        [TestMethod]
        public void DeveTestar_Depositar_Exception()
        {
            ContaDTO conta = new ContaDTO();
            decimal valorDepositar = 200;
            string notasUtilizada = "1,1,1,1";

            mockOperacoesDao.Setup(x => x.Depositar(It.IsAny<ContaDTO>(), It.IsAny<decimal>(), It.IsAny<string>()))
                  .Throws(new Exception("erro", new Exception("innter erro")));
            service.Depositar(conta, valorDepositar, notasUtilizada);
        }
    }
}
