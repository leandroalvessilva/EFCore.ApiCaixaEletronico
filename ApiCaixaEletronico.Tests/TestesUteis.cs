using ApiCaixaEletronico.DAO;
using ApiCaixaEletronico.DTO.Context;
using ApiCaixaEletronico.DTO.DTO;
using Microsoft.EntityFrameworkCore;

namespace ApiCaixaEletronico.Tests
{
    public class TestesUteis
    {
        public DbContextOptions<CommonDbContext> CriarDataBaseTeste(string DataBaseTeste)
        {
            return new DbContextOptionsBuilder<CommonDbContext>().UseInMemoryDatabase(databaseName: DataBaseTeste).Options;
        }

        public ContaContext ListarContas()
        {
            ContaContext conta = new ContaContext()
            {
                Agencia = 2020,
                Banco = 341,
                CpfCliente = 12345678900,
                IdConta = 001,
                NumeroContaCli = 20203411,
                SaldoConta = 1000,
                SenhaConta = 12346,
                TipoConta = 01
            };
            return conta;
        }

        public ClienteContext ListarClientes()
        {
            ClienteContext conta = new ClienteContext()
            {
                CpfCli = 12345678900,
                Email = "teste@teste.com",
                Id_Cliente = 1,
                Nome_Cliente = "Teste",
                RgCli = 12344567,
                Telefone = 1123234545
            };
            return conta;
        }

        public CaixaEletronicoContext ListarCaixas()
        {
            CaixaEletronicoContext caixa = new CaixaEletronicoContext()
            {
                Id_Terminal = 1,
                NotasCem = 100,
                NotasCinquenta = 100,
                NotasDez = 100,
                NotasVinte = 100,
                Valor_Disponivel = 10800            
            };
            return caixa;
        }

        public ContaDTO Contas()
        {
            ContaDTO contas = new ContaDTO()
            {
                AgenciaContaCli = 2020,
                BancoContaCli = 341,
                CpfCli = 12345678900,
                NumeroContaCli = 20203411,
                SaldoConta = 1000
            };

            return contas;
        }

        public ContasTransferenciaDTO ContasTransferencia()
        {
            ContasTransferenciaDTO contasTransferencia = new ContasTransferenciaDTO()
            {
                Usuario_Agencia = 2020,
                Usuario_Banco = 341,
                Usuario_Cpf = 12345678900,
                Usuario_NumeroConta = 20203411,
                Destino_Agencia = 2020,
                Destino_Banco = 341,
                Destino_Cpf = 12345678900,
                Destino_NumeroConta = 20203411
            };
            return contasTransferencia;
        }
    }
}
