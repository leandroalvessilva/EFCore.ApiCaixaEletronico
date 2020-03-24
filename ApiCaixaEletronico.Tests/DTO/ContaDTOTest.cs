using ApiCaixaEletronico.DTO.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.Tests.DTO
{
    [TestClass]
    public class ContaDTOTest
    {
        [TestMethod]
        public void ContaDTOTeste()
        {
            var obj = new ContaDTO()
            {
                AgenciaContaCli = 2020,
                BancoContaCli = 341,
                CpfCli = 123456787900,
                NumeroContaCli = 20203411,
                SaldoConta = 1000
            };

            Assert.AreEqual(2020, obj.AgenciaContaCli);
            Assert.AreEqual(341, obj.BancoContaCli);
            Assert.AreEqual(123456787900, obj.CpfCli);
            Assert.AreEqual(20203411, obj.NumeroContaCli);
            Assert.AreEqual(1000, obj.SaldoConta);
        }
    }
}
