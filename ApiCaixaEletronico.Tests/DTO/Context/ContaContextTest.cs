using ApiCaixaEletronico.DTO.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiCaixaEletronico.Tests.DTO.Context
{
    [TestClass]
    public class ContaContextTest
    {
        [TestMethod]
        public void ContaContextTeste()
        {
            var obj = new ContaContext()
            {
                Agencia = 2020,
                Banco = 341,
                CpfCliente = 123456787900,
                IdConta = 1,
                SenhaConta = 123456,
                TipoConta = 1,
                NumeroContaCli = 20203411,
                SaldoConta = 1000
            };

            Assert.AreEqual(2020, obj.Agencia);
            Assert.AreEqual(341, obj.Banco);
            Assert.AreEqual(123456787900, obj.CpfCliente);
            Assert.AreEqual(1, obj.IdConta);
            Assert.AreEqual(123456, obj.SenhaConta);
            Assert.AreEqual(1, obj.TipoConta);
            Assert.AreEqual(20203411, obj.NumeroContaCli);
            Assert.AreEqual(1000, obj.SaldoConta);
        }
    }
}
