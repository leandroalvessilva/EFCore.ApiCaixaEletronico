using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCaixaEletronico.DTO.Context
{
    [Table("TBDADOSCLIENTES")]
    public class ClienteContext
    {
        [Column("ID_CLIENTE")]
        public int Id_Cliente { get; set; }

        [Column("NOME_CLI")]
        public string Nome_Cliente { get; set; }

        [Column("CPF_CLI")]
        public long CpfCli { get; set; }

        [Column("RG_CLI")]
        public long RgCli { get; set; }

        [Column("TELEFONE")]
        public long Telefone { get; set; }

        [Column("EMAIL")]
        public string Email { get; set; }
    }
}
