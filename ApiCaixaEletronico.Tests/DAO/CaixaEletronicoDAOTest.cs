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
    public class CaixaEletronicoDAOTest
    {
        private readonly Mock<IConfiguration> config;
        private readonly TestesUteis testesUteis;
        private readonly Mock<IOperacoesBancariasDAO> mockOperacoes;
        private readonly Mock<ICaixaEletronicoDAO> mockCaixaEletronico;
        private CaixaEletronicoDAO caixaEletronicoDao;

        public CaixaEletronicoDAOTest()
        {
            config = new Mock<IConfiguration>();
            mockOperacoes = new Mock<IOperacoesBancariasDAO>();
            mockCaixaEletronico = new Mock<ICaixaEletronicoDAO>();
            testesUteis = new TestesUteis();
        }

        [TestMethod]
        public void CaixaEletronicoDAOTest_CalcularNotasNecessarias()
        {
            var options = testesUteis.CriarDataBaseTeste("CalcularNotasNecessariasDAOTeste");

            int[] notasNecessarias = new int[4] { 1, 1, 1, 1 };

            using (var context = new CommonDbContext(options))
            {
                context.Caixas.Add(testesUteis.ListarCaixas());

                context.SaveChanges();

                caixaEletronicoDao = new CaixaEletronicoDAO(context);

                var result = caixaEletronicoDao.CalcularNotasNecessarias(notasNecessarias);

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void CaixaEletronicoDAOTest_RetornarNotasNecessarias()
        {
            var options = testesUteis.CriarDataBaseTeste("RetornarNotasNecessariasDAOTeste");

            decimal valorSacar = 500;

            using (var context = new CommonDbContext(options))
            {
                context.Caixas.Add(testesUteis.ListarCaixas());

                context.SaveChanges();

                caixaEletronicoDao = new CaixaEletronicoDAO(context);

                var result = caixaEletronicoDao.RetornarNotasNecessarias(valorSacar);

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void CaixaEletronicoDAOTest_ValidarSaque()
        {
            var options = testesUteis.CriarDataBaseTeste("ValidarSaqueDAOTeste");

            decimal valorSacar = 500;

            using (var context = new CommonDbContext(options))
            {
                caixaEletronicoDao = new CaixaEletronicoDAO(context);

                var result = caixaEletronicoDao.ValidarSaque(valorSacar, testesUteis.ListarContas(), testesUteis.ListarCaixas());
               
                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void CaixaEletronicoDAOTest_ValidarInformacoes()
        {
            var options = testesUteis.CriarDataBaseTeste("ValidarInformacoesDAOTeste");

            using (var context = new CommonDbContext(options))
            {
                context.Caixas.Add(testesUteis.ListarCaixas());

                context.SaveChanges();

                caixaEletronicoDao = new CaixaEletronicoDAO(context);

                var result = caixaEletronicoDao.ValidarInformacoes(testesUteis.Contas(), testesUteis.Contas());

                Assert.IsNotNull(result);
            }
        }

    }
}
