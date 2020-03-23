using ApiCaixaEletronico.DTO;
using ApiCaixaEletronico.DTO.DTO;

namespace ApiCaixaEletronico.Service.Interface
{
    public interface IOperacoesBancariasService
    {
        Retorno Saldo(ContaDTO conta);
        
        Retorno Sacar(ContaDTO conta, bool isTransferencia, decimal valorSacar);
               
        Retorno Depositar(ContaDTO conta, bool outraConta, decimal ValorDepositar);

        Retorno Transferir(ContaDTO conta, ContaDTO contaDestino, decimal ValorTransferir);
    }
}
