using System.Runtime.Serialization;

namespace ApiCaixaEletronico.DTO
{
    [DataContract(Name = "Retorno")]
    public class Retorno
    {
        [DataMember(Name = "Codigo")]
        public int Codigo { get; set; }

        [DataMember(Name = "Mensagem")]
        public string Mensagem { get; set; }

        [DataMember(Name = "Data")]
        public string Data { get; set; }
    }
}
