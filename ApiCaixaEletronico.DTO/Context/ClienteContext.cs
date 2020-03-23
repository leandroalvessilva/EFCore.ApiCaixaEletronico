using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCaixaEletronico.DTO.Context
{
    [Table("TBDADOSCLIENTES")]
    public class ClienteContext
    {
        [Column("ID_CLIENTE")]
        public int Id_Cliente { get; set; }

        [Column("NOME_CLI")]
        public int Nome_Cliente { get; set; }

        [Column("CPF_CLI")]
        public int CpfCli { get; set; }

        [Column("RG_CLI")]
        public int RgCli { get; set; }

        [Column("TELEFONE")]
        public int Telefone { get; set; }

        [Column("EMAIL")]
        public int Email { get; set; }
    }
}
