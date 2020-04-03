using ApiCaixaEletronico.DTO;
using ApiCaixaEletronico.DTO.DTO;

namespace ApiCaixaEletronico.Service.Interface
{
    public interface IOperacoesBancariasService
    {
        Retorno Saldo(int banco, int agencia, int numeroConta, long cpf);

        Retorno Sacar(ContaDTO conta, decimal valorSacar);

        Retorno Depositar(ContaDTO conta, decimal valorDepositar, string notasDepositadas);
    }
}
