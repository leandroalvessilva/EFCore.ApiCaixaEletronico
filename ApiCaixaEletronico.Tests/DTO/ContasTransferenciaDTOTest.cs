using ApiCaixaEletronico.DTO.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.Tests.DTO
{
    [TestClass]
    public class ContasTransferenciaDTOTest
    {
        [TestMethod]
        public void ContasTransferenciaDTOTeste()
        {
            TestesUteis testesUteis = new TestesUteis();

            var obj = testesUteis.ContasTransferencia();

            Assert.AreEqual(2020, obj.Usuario_Agencia);
            Assert.AreEqual(123, obj.Usuario_Banco);
            Assert.AreEqual(12345678900, obj.Usuario_Cpf);
            Assert.AreEqual(20201231, obj.Usuario_NumeroConta);
            Assert.AreEqual(2021, obj.Destino_Agencia);
            Assert.AreEqual(124, obj.Destino_Banco);
            Assert.AreEqual(12345678901, obj.Destino_Cpf);
            Assert.AreEqual(20211241, obj.Destino_NumeroConta);
        }
    }
}
