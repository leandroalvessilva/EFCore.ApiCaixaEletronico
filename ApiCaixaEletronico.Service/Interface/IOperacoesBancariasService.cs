using ApiCaixaEletronico.DTO;
using ApiCaixaEletronico.DTO.DTO;

namespace ApiCaixaEletronico.Service.Interface
{
    public interface IOperacoesBancariasService
    {
        Retorno Saldo(int banco, int agencia, int numeroConta, long cpf);

        Retorno Sacar(ContaDTO conta, bool isTransferencia, decimal valorSacar);
               
        Retorno Depositar(ContaDTO conta, bool outraConta, decimal ValorDepositar);

        Retorno Transferir(ContasTransferenciaDTO contasTransferencia, decimal ValorTransferir);
    }
}
