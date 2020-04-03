using ApiCaixaEletronico.DTO.Context;
using ApiCaixaEletronico.DTO.DTO;

namespace ApiCaixaEletronico.DAO.Interface
{
    public interface ICaixaEletronicoDAO
    {
        int[] ConsultarNotasDisponiveis();

        int[] CalcularNotasNecessarias(int[] notasNecessarias);

        int[] RetornarNotasNecessarias(decimal ValorSacar);

        bool ValidarSaque(decimal valorSacar, ContaContext contaUsuario, CaixaEletronicoContext caixaEletronico);

        bool ValidarDeposito(ContaContext conta, decimal valorDepositar, string[] notasDepositadas);

        bool ValidarInformacoes(ContasTransferenciaDTO contasTransferencia);

        bool Login(long cpf, int senha);

        ContaDTO ListarUsuario(long cpf);
    }
}
