using ApiCaixaEletronico.DAO;
using ApiCaixaEletronico.DAO.DAO;
using ApiCaixaEletronico.DAO.Interface;
using ApiCaixaEletronico.DTO.Context;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

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

                var result = OperacoesBancariasDao.Saldo(testesUteis.Contas().BancoContaCli, testesUteis.Contas().AgenciaContaCli, testesUteis.Contas().NumeroContaCli, testesUteis.Contas().CpfCli);

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

                mockCaixaEletronico.Setup(x => x.ValidarSaque(It.IsAny<decimal>(), It.IsAny<ContaContext>(), It.IsAny<CaixaEletronicoContext>())).Returns(true);

                int[] arrayRetorno = new int[4];

                mockCaixaEletronico.Setup(x => x.RetornarNotasNecessarias(It.IsAny<decimal>())).Returns(arrayRetorno);

                var result = OperacoesBancariasDao.Sacar(testesUteis.Contas(), valorSacar);

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void OperacoesBancariasDAOTest_SacarC1()
        {
            var options = testesUteis.CriarDataBaseTeste("SacarDAOC1Teste");
            decimal valorSacar = 100;

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());
                context.Caixas.Add(testesUteis.ListarCaixas());

                context.SaveChanges();

                OperacoesBancariasDao = new OperacoesBancariasDAO(context, mockCaixaEletronico.Object);

                var result = OperacoesBancariasDao.Sacar(testesUteis.Contas(), valorSacar);

                Assert.IsNotNull(result);
            }
        }

        [TestMethod]
        public void OperacoesBancariasDAOTest_Depositar()
        {
            var options = testesUteis.CriarDataBaseTeste("DepositarDAOTeste");
            decimal valorDepositar = 100;
            string notasUtilizadas = "1,1,1,1";

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());

                context.SaveChanges();

                OperacoesBancariasDao = new OperacoesBancariasDAO(context, mockCaixaEletronico.Object);

                var result = OperacoesBancariasDao.Depositar(testesUteis.Contas(), valorDepositar, notasUtilizadas);

                Assert.AreEqual(false,result);
            }
        }

        [TestMethod]
        public void OperacoesBancariasDAOTest_DepositarC1()
        {
            var options = testesUteis.CriarDataBaseTeste("DepositarDAOC1Teste");
            decimal valorDepositar = 100;
            string notasUtilizadas = "1,1,1,1";

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testesUteis.ListarContas());
                context.Caixas.Add(testesUteis.ListarCaixas());

                context.SaveChanges();

                OperacoesBancariasDao = new OperacoesBancariasDAO(context, mockCaixaEletronico.Object);

                mockCaixaEletronico.Setup(x => x.ValidarDeposito(It.IsAny<ContaContext>(), It.IsAny<decimal>(), It.IsAny<string[]>())).Returns(true);

                var result = OperacoesBancariasDao.Depositar(testesUteis.Contas(), valorDepositar, notasUtilizadas);

                Assert.AreEqual(true, result);
            }
        }
    }
}
