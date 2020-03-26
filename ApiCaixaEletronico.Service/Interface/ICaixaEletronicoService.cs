using ApiCaixaEletronico.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiCaixaEletronico.Service.Interface
{
    public interface ICaixaEletronicoService
    {
        Retorno Login(long cpf, int senha);

        Retorno ListarUsuario(long cpf, int senha);
    }
}
