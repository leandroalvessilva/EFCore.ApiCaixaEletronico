using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.DTO.DTO
{
    public class CaixaEletronicoDTO
    {
        public int Id_Terminal { get; set; }
        public decimal Valor_Disponivel { get; set; }
        public int NotasCem { get; set; }
        public int NotasCinquenta { get; set; }
        public int NotasVinte { get; set; }
        public int NotasDez { get; set; }
    }
}
