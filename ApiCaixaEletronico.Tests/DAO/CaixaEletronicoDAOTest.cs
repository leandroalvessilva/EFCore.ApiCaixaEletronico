using ApiCaixaEletronico.DAO;
using ApiCaixaEletronico.DAO.DAO;
using ApiCaixaEletronico.DAO.Interface;
using ApiCaixaEletronico.DTO.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

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
        public void CaixaEletronicoDAOTest_CalcularNotasNecessariasC2()
        {
            var options = testesUteis.CriarDataBaseTeste("CalcularNotasNecessariasC2DAOTeste");

            int[] notasNecessarias = new int[4] { 101, 101, 110, 10 };

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
        [ExpectedException(typeof(Exception))]
        public void CaixaEletronicoDAOTest_CalcularNotasNecessariasException()
        {
            var options = testesUteis.CriarDataBaseTeste("CalcularNotasNecessariasExceptionDAOTeste");

            int[] notasNecessarias = new int[4] { 101, 101, 110, 101 };

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
        [ExpectedException(typeof(Exception))]
        public void CaixaEletronicoDAOTest_ValidarSaqueC1()
        {
            var options = testesUteis.CriarDataBaseTeste("ValidarSaqueDAOC1Teste");

            decimal valorSacar = 2000;

            using (var context = new CommonDbContext(options))
            {
                caixaEletronicoDao = new CaixaEletronicoDAO(context);

                var result = caixaEletronicoDao.ValidarSaque(valorSacar, testesUteis.ListarContas(), testesUteis.ListarCaixas());
            }
        }

        [TestMethod]
        public void CaixaEletronicoDAOTest_ValidarSaqueC2()
        {
            var options = testesUteis.CriarDataBaseTeste("ValidarSaqueDAOC2Teste");

            decimal valorSacar = 88;

            using (var context = new CommonDbContext(options))
            {
                caixaEletronicoDao = new CaixaEletronicoDAO(context);

                var result = caixaEletronicoDao.ValidarSaque(valorSacar, testesUteis.ListarContas(), testesUteis.ListarCaixas());

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CaixaEletronicoDAOTest_ValidarSaqueC3()
        {
            var options = testesUteis.CriarDataBaseTeste("ValidarSaqueDAOC3Teste");

            decimal valorSacar = 1000;

            ContaContext conta = testesUteis.ListarContas();

            conta.SaldoConta = 5000;

            CaixaEletronicoContext caixa = testesUteis.ListarCaixas();

            caixa.Valor_Disponivel = 500;

            using (var context = new CommonDbContext(options))
            {
                caixaEletronicoDao = new CaixaEletronicoDAO(context);

                var result = caixaEletronicoDao.ValidarSaque(valorSacar, conta, caixa);
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

                var result = caixaEletronicoDao.ValidarInformacoes(testesUteis.ContasTransferencia());

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void CaixaEletronicoDAOTest_ValidarInformacoesC1()
        {
            var options = testesUteis.CriarDataBaseTeste("ValidarInformacoesC1DAOTeste");

            using (var context = new CommonDbContext(options))
            {
                context.Caixas.Add(testesUteis.ListarCaixas());

                context.Contas.Add(testesUteis.ListarContas());

                context.SaveChanges();

                caixaEletronicoDao = new CaixaEletronicoDAO(context);

                var result = caixaEletronicoDao.ValidarInformacoes(testesUteis.ContasTransferencia());

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void CaixaEletronicoDAOTest_Login()
        {
            var options = testesUteis.CriarDataBaseTeste("LoginDAOTeste");

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());

                context.SaveChanges();

                caixaEletronicoDao = new CaixaEletronicoDAO(context);

                var result = caixaEletronicoDao.Login(testesUteis.ListarContas().CpfCliente, testesUteis.ListarContas().SenhaConta);

                Assert.AreEqual(true, result);
            }
        }

        [TestMethod]
        public void CaixaEletronicoDAOTest_LoginC1()
        {
            var options = testesUteis.CriarDataBaseTeste("LoginDAOC1Teste");

            using (var context = new CommonDbContext(options))
            {
                caixaEletronicoDao = new CaixaEletronicoDAO(context);

                var result = caixaEletronicoDao.Login(testesUteis.ListarContas().CpfCliente, testesUteis.ListarContas().SenhaConta);

                Assert.AreEqual(false, result);
            }
        }

        [TestMethod]
        public void CaixaEletronicoDAOTest_ListarUsuario()
        {
            var options = testesUteis.CriarDataBaseTeste("ListarUsuarioDAOTeste");

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());
                context.Clientes.Add(testesUteis.ListarClientes());

                context.SaveChanges();

                caixaEletronicoDao = new CaixaEletronicoDAO(context);

                var result = caixaEletronicoDao.ListarUsuario(testesUteis.ListarContas().CpfCliente);

                Assert.IsNotNull(result);
            }
        }
    }
}
