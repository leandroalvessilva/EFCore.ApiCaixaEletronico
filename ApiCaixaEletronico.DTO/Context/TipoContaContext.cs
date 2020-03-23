using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCaixaEletronico.DTO.Context
{
    [Table("TBTIPOCONTA")]
    public class TipoContaContext
    {
        [Column("COD_TIPCONTA")]
        public int Codigo_TipoConta { get; set; }

        [Column("DESC_TIPCONTA")]
        public int Descricao_TipoConta { get; set; }        
    }
}
