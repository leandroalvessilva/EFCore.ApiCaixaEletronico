using ApiCaixaEletronico.DTO.DTO;
using System;

namespace ApiCaixaEletronico.DAO.DAO
{
    public interface IOperacoesBancariasDAO
    {
        decimal Saldo(ContaDTO conta);

        OperacaoDTO Sacar(ContaDTO conta, bool isTransferencia, decimal valorSacar);

        bool Depositar(ContaDTO conta, bool outraConta, decimal ValorDepositar);

        bool Transferir(ContaDTO conta, ContaDTO contaDestino, decimal ValorTransferir);
    }
}
