using ApiCaixaEletronico.DTO;

namespace ApiCaixaEletronico.Service.Interface
{
    public interface ICaixaEletronicoService
    {
        Retorno Login(long cpf, int senha);

        Retorno ListarUsuario(long cpf);
    }
}
