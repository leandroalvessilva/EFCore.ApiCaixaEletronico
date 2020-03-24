using ApiCaixaEletronico.DTO.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.Tests.DTO.Context
{
    [TestClass]
    public class AgenciaContextTest
    {
        [TestMethod]
        public void AgenciaContextTeste()
        {
            var obj = new AgenciaContext()
            {
                Id_Agencia = 001,
                Nome_Agencia = "Teste"
            };

            Assert.AreEqual(001, obj.Id_Agencia);
            Assert.AreEqual("Teste", obj.Nome_Agencia);
        }
    }
}
