using ApiCaixaEletronico.DAO;
using ApiCaixaEletronico.DAO.Interface;
using ApiCaixaEletronico.DTO.DTO;
using ApiCaixaEletronico.Service.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace ApiCaixaEletronico.Tests.Services
{
    [TestClass]
    public class CaixaEletronicoServiceTeste
    {
        private CaixaEletronicoService service;
        private Mock<ICaixaEletronicoDAO> mockCaixaEletronicoDao;
        private TestesUteis testeUteis;

        [TestInitialize]
        public void Initialize()
        {
            mockCaixaEletronicoDao = new Mock<ICaixaEletronicoDAO>();
            service = new CaixaEletronicoService(mockCaixaEletronicoDao.Object);
            testeUteis = new TestesUteis();
        }

        [TestMethod]
        public void DeveTestar_Login()
        {
            var options = testeUteis.CriarDataBaseTeste("LoginTeste");

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testeUteis.ListarContas());

                context.SaveChanges();

                var chamada = service.Login(testeUteis.ListarContas().CpfCliente, testeUteis.ListarContas().SenhaConta);

                Assert.AreEqual(500,chamada.Codigo);
                Assert.IsNull(chamada.Data);
                Assert.AreEqual("Erro ao realizar login, verifique as informações.", chamada.Mensagem);
            }
        }

        [TestMethod]
        public void DeveTestar_LoginC1()
        {
            var options = testeUteis.CriarDataBaseTeste("LoginC1Teste");

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testeUteis.ListarContas());

                context.SaveChanges();

                mockCaixaEletronicoDao.Setup(x => x.Login(It.IsAny<long>(), It.IsAny<int>())).Returns(true);

                var chamada = service.Login(testeUteis.ListarContas().CpfCliente, testeUteis.ListarContas().SenhaConta);

                Assert.AreEqual(200, chamada.Codigo);
                Assert.IsNotNull(chamada.Data);
                Assert.AreEqual("Login efetuado com sucesso.", chamada.Mensagem);
            }
        }

        [TestMethod]
        public void DeveTestar_Login_Exception()
        {
            mockCaixaEletronicoDao.Setup(x => x.Login(It.IsAny<long>(), It.IsAny<int>()))
                  .Throws(new Exception("erro", new Exception("innter erro")));
            service.Login(testeUteis.ListarContas().CpfCliente, testeUteis.ListarContas().SenhaConta);
        }

        [TestMethod]
        public void DeveTestar_ListarUsuario()
        {
            var options = testeUteis.CriarDataBaseTeste("ListarUsuarioTeste");

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testeUteis.ListarContas());

                context.SaveChanges();

                mockCaixaEletronicoDao.Setup(x => x.ListarUsuario(It.IsAny<long>())).Returns(new ContaDTO());

                var chamada = service.ListarUsuario(testeUteis.ListarContas().CpfCliente);

                Assert.AreEqual(500, chamada.Codigo);
                Assert.IsNull(chamada.Data);
                Assert.AreEqual("Erro ao listar usuário, verifique as informações.", chamada.Mensagem);
            }
        }

        [TestMethod]
        public void DeveTestar_ListarUsuarioC1()
        {
            var options = testeUteis.CriarDataBaseTeste("ListarUsuarioC1Teste");

            using (var context = new CommonDbContext(options))
            {
                context.Contas.Add(testeUteis.ListarContas());

                context.SaveChanges();

                mockCaixaEletronicoDao.Setup(x => x.ListarUsuario(It.IsAny<long>())).Returns(testeUteis.Contas());

                var chamada = service.ListarUsuario(testeUteis.ListarContas().CpfCliente);

                Assert.AreEqual(200, chamada.Codigo);
                Assert.IsNotNull(chamada.Data);
                Assert.AreEqual("Consulta efetuada com sucesso", chamada.Mensagem);
            }
        }

        [TestMethod]
        public void DeveTestar_ListarUsuario_Exception()
        {
            mockCaixaEletronicoDao.Setup(x => x.Login(It.IsAny<long>(), It.IsAny<int>()))
                  .Throws(new Exception("erro", new Exception("innter erro")));
            service.ListarUsuario(testeUteis.ListarContas().CpfCliente);
        }
    }
}