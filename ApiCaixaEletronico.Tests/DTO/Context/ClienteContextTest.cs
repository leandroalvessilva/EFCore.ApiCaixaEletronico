using ApiCaixaEletronico.DTO.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.Tests.DTO.Context
{
    [TestClass]
    public class ClienteContextTest
    {
        [TestMethod]
        public void ClienteContextTeste()
        {
            var obj = new ClienteContext()
            {
                CpfCli = 12345678900,
                Email = "teste@teste.com",
                Id_Cliente = 01,
                Nome_Cliente = "teste",
                RgCli = 12345678,
                Telefone = 119923456677                
            };

            Assert.AreEqual(12345678900, obj.CpfCli);
            Assert.AreEqual("teste@teste.com", obj.Email);
            Assert.AreEqual(01, obj.Id_Cliente);
            Assert.AreEqual("teste", obj.Nome_Cliente);
            Assert.AreEqual(12345678, obj.RgCli);
            Assert.AreEqual(119923456677, obj.Telefone);
        }
    }
}
