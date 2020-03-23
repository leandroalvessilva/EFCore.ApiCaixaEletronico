using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCaixaEletronico.DTO.Context
{
    [Table("TBAGENCIACONTA")]
    public class AgenciaContext
    {
        [Column("ID_AGENCIA")]
        public int Id_Agencia { get; set; }

        [Column("NOME")]
        public int Nome_Agencia { get; set; }        
    }
}
