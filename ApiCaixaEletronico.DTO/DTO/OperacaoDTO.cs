using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.DTO.DTO
{
    public class OperacaoDTO
    {
        public ContaDTO Conta { get; set; }

        public string[] NotasUtilizadas { get; set; }

        public decimal ValorSacado { get; set; }

        public bool Realizada { get; set; }
    }
}
