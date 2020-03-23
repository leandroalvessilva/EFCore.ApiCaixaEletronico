using ApiCaixaEletronico.DTO.Context;
using ApiCaixaEletronico.DTO.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.DAO.Interface
{
    public interface ICaixaEletronicoDAO
    {
        int[] ConsultarNotasDisponiveis();

        int[] CalcularNotasNecessarias(int[] notasNecessarias);

        int[] RetornarNotasNecessarias(decimal ValorSacar);

        bool ValidarSaque(decimal valorSacar, ContaContext contaUsuario, CaixaEletronicoContext caixaEletronico);

        bool ValidarInformacoes(ContaDTO conta, ContaDTO contaDestino);
    }
}
