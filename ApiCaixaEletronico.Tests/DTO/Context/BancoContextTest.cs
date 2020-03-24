using ApiCaixaEletronico.DTO.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.Tests.DTO.Context
{
    [TestClass]
    public class BancoContextTest
    {
        [TestMethod]
        public void BancoContextTeste()
        {
            var obj = new BancoContext()
            {
                Id_Banco = 341,
                Nome = "Teste"
            };

            Assert.AreEqual(341, obj.Id_Banco);
            Assert.AreEqual("Teste", obj.Nome);
        }
    }
}
