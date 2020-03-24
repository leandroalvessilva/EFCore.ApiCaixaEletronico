using ApiCaixaEletronico.DTO.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.Tests.DTO.Context
{
    [TestClass]
    public class TipoContaContextTest
    {
        [TestMethod]
        public void TipoContaContexTeste()
        {
            var obj = new TipoContaContext()
            {
                Codigo_TipoConta = 1,
                Descricao_TipoConta = "Teste"
            };

            Assert.AreEqual(1, obj.Codigo_TipoConta);
            Assert.AreEqual("Teste", obj.Descricao_TipoConta);
        }
    }
}
