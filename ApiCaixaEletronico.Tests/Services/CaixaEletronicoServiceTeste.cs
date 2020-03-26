using ApiCaixaEletronico.DAO;
using ApiCaixaEletronico.DAO.Interface;
using ApiCaixaEletronico.Service.Service;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

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

                Assert.IsNotNull(chamada);
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

                var chamada = service.ListarUsuario(testeUteis.ListarContas().CpfCliente, testeUteis.ListarContas().SenhaConta);

                Assert.IsNotNull(chamada);
            }
        }

        [TestMethod]
        public void DeveTestar_ListarUsuario_Exception()
        {
            mockCaixaEletronicoDao.Setup(x => x.Login(It.IsAny<long>(), It.IsAny<int>()))
                  .Throws(new Exception("erro", new Exception("innter erro")));
            service.ListarUsuario(testeUteis.ListarContas().CpfCliente, testeUteis.ListarContas().SenhaConta);
        }
    }
}
