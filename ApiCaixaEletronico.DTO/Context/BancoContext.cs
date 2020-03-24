using System.ComponentModel.DataAnnotations.Schema;

namespace ApiCaixaEletronico.DTO.Context
{
    [Table("TBBANCO")]
    public class BancoContext
    {
        [Column("ID_BANCO")]
        public int Id_Banco { get; set; }

        [Column("NOME")]
        public string Nome { get; set; }        
    }
}
