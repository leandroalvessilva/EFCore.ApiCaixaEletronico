using ApiCaixaEletronico.DAO;
using ApiCaixaEletronico.DAO.DAO;
using ApiCaixaEletronico.DAO.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.Tests.DAO
{
    [TestClass]
    public class OperacoesBancariasDAOTest
    {
        private readonly Mock<IConfiguration> config;
        private readonly TestesUteis testesUteis;
        private readonly Mock<IOperacoesBancariasDAO> mockOperacoes;
        private readonly Mock<ICaixaEletronicoDAO> mockCaixaEletronico;
        private OperacoesBancariasDAO OperacoesBancariasDao;

        public OperacoesBancariasDAOTest()
        {
            config = new Mock<IConfiguration>();
            mockOperacoes = new Mock<IOperacoesBancariasDAO>();
            mockCaixaEletronico = new Mock<ICaixaEletronicoDAO>();
            testesUteis = new TestesUteis();
        }

        [TestMethod]
        public void OperacoesBancariasDAOTest_Saldo()
        {
            var options = testesUteis.CriarDataBaseTeste("SaldoDAOTeste");

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());

                context.SaveChanges();

                OperacoesBancariasDao = new OperacoesBancariasDAO(context, mockCaixaEletronico.Object);

                var result = OperacoesBancariasDao.Saldo(testesUteis.Contas());

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void OperacoesBancariasDAOTest_Sacar()
        {
            var options = testesUteis.CriarDataBaseTeste("SacarDAOTeste");
            decimal valorSacar = 100;

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());
                context.Caixas.Add(testesUteis.ListarCaixas());

                context.SaveChanges();

                OperacoesBancariasDao = new OperacoesBancariasDAO(context, mockCaixaEletronico.Object);

                var result = OperacoesBancariasDao.Sacar(testesUteis.Contas(), false, valorSacar);

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void OperacoesBancariasDAOTest_Depositar()
        {
            var options = testesUteis.CriarDataBaseTeste("DepositarDAOTeste");
            decimal valorDepositar = 100;

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());

                context.SaveChanges();

                OperacoesBancariasDao = new OperacoesBancariasDAO(context, mockCaixaEletronico.Object);

                var result = OperacoesBancariasDao.Depositar(testesUteis.Contas(), false, valorDepositar);

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void OperacoesBancariasDAOTest_Transferir()
        {
            var options = testesUteis.CriarDataBaseTeste("TransferirDAOTeste");
            decimal valorTransferir = 100;

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());

                context.SaveChanges();

                OperacoesBancariasDao = new OperacoesBancariasDAO(context, mockCaixaEletronico.Object);

                var result = OperacoesBancariasDao.Transferir(testesUteis.Contas(), testesUteis.Contas(), valorTransferir);

                Assert.IsNotNull(result);
            }
        }
    }
}
