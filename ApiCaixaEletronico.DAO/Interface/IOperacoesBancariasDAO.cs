using ApiCaixaEletronico.DTO.DTO;

namespace ApiCaixaEletronico.DAO.DAO
{
    public interface IOperacoesBancariasDAO
    {
        decimal Saldo(int banco, int agencia, int numeroConta, long cpf);

        OperacaoDTO Sacar(ContaDTO conta, decimal valorSacar);

        bool Depositar(ContaDTO conta, decimal valorDepositar, string notasDepositadas);
    }
}
