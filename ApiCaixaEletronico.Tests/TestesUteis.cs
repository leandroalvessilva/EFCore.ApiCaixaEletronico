using ApiCaixaEletronico.DAO;
using ApiCaixaEletronico.DTO.Context;
using ApiCaixaEletronico.DTO.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
