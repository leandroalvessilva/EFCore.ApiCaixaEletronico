using ApiCaixaEletronico.DTO.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ApiCaixaEletronico.Tests.DTO
{
    [TestClass]
    public class OperacaoDTOTest
    {
        [TestMethod]
        public void OperacaoDTOTeste()
        {
            TestesUteis testesUteis = new TestesUteis();

            var obj = new OperacaoDTO()
            {
                Conta = testesUteis.Contas(),
                NotasUtilizadas = new int[4] {1, 1, 1, 1},
                Realizada = true,
                ValorSacado = 500
            };

            Assert.IsInstanceOfType(obj.Conta, typeof(ContaDTO));
            Assert.IsInstanceOfType(obj.NotasUtilizadas, typeof(int[]));
            Assert.AreEqual(true, obj.Realizada);
            Assert.AreEqual(500, obj.ValorSacado);
        }
    }
}
