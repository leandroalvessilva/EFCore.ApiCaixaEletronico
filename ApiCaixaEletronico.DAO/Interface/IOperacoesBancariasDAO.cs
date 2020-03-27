using ApiCaixaEletronico.DTO.DTO;
using System;

namespace ApiCaixaEletronico.DAO.DAO
{
    public interface IOperacoesBancariasDAO
    {
        decimal Saldo(int banco, int agencia, int numeroConta, long cpf);

        OperacaoDTO Sacar(ContaDTO conta, bool isTransferencia, decimal valorSacar);

        bool Depositar(ContaDTO conta, bool outraConta, decimal ValorDepositar);

        bool Transferir(ContasTransferenciaDTO contasTransferencia, decimal ValorTransferir);
    }
}
