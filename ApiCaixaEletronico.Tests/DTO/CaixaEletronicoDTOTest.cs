﻿using ApiCaixaEletronico.DTO.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.Tests.DTO
{
    [TestClass]
    public class CaixaEletronicoDTOTest
    {
        [TestMethod]
        public void CaixaEletronicoDTOTeste()
        {
            var obj = new CaixaEletronicoDTO()
            {
                Id_Terminal = 1,
                NotasCem = 100,
                NotasCinquenta = 50,
                NotasDez = 10,
                NotasVinte = 20,
                Valor_Disponivel = 10000
            };

            Assert.AreEqual(1, obj.Id_Terminal);
            Assert.AreEqual(100, obj.NotasCem);
            Assert.AreEqual(50, obj.NotasCinquenta);
            Assert.AreEqual(10, obj.NotasDez);
            Assert.AreEqual(20, obj.NotasVinte);
            Assert.AreEqual(10000, obj.Valor_Disponivel);
        }
    }
}
