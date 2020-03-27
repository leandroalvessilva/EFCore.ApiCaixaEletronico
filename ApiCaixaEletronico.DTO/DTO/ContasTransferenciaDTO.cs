using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.DTO.DTO
{
    public class ContasTransferenciaDTO
    {
        public int Usuario_Banco { get; set; }

        public int Usuario_Agencia { get; set; }

        public int Usuario_NumeroConta { get; set; }

        public long Usuario_Cpf { get; set; }

        public int Destino_Banco { get; set; }

        public int Destino_Agencia { get; set; }

        public int Destino_NumeroConta { get; set; }

        public long Destino_Cpf { get; set; }
    }
}
