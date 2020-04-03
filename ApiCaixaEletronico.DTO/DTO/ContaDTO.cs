namespace ApiCaixaEletronico.DTO.DTO
{
    public class ContaDTO
    {
        public int BancoContaCli { get; set; }

        public int AgenciaContaCli { get; set; }

        public int NumeroContaCli { get; set; }

        public decimal SaldoConta { get; set; }

        public long CpfCli { get; set; }

        public string Nome_Cliente { get; set; }
    }
}

