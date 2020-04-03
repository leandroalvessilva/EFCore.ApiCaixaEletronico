using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCaixaEletronico.DTO.Context
{
    [Table("TBCONTASCLIENTES")]
    public class ContaContext
    {
        [Column("IDCONTA")]
        public int IdConta { get; set; }

        [Column("COD_TIPCONTA")]
        public int TipoConta { get; set; }

        [Column("SENHACONTA")]
        public int SenhaConta { get; set; }

        [Column("ID_BANCO")]
        public int Banco { get; set; }

        [Column("ID_AGENCIA")]
        public int Agencia { get; set; }

        [Column("NROCONT")]
        public int NumeroContaCli { get; set; }

        [Column("SALDOCONT")]
        public decimal SaldoConta { get; set; }

        [Column("CPFCLI")]
        public long CpfCliente { get; set; }
    }
}
