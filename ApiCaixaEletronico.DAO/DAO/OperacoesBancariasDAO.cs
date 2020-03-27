using ApiCaixaEletronico.DAO.Interface;
using ApiCaixaEletronico.DTO.DTO;
using System.Linq;

namespace ApiCaixaEletronico.DAO.DAO
{
    public class OperacoesBancariasDAO : IOperacoesBancariasDAO
    {
        private CommonDbContext _commonDbContext;
        private ICaixaEletronicoDAO _caixaEletronicoDAO;

        public OperacoesBancariasDAO(CommonDbContext commonDbContext, ICaixaEletronicoDAO caixaEletronicoDAO)
        {
            this._commonDbContext = commonDbContext;
            this._caixaEletronicoDAO = caixaEletronicoDAO;
        }

        public decimal Saldo(int banco, int agencia, int numeroConta, long cpf)
        {
            decimal saldo = _commonDbContext.Contas.Where(x => x.Banco == banco && x.Agencia == agencia && x.NumeroContaCli == numeroConta && x.CpfCliente == cpf).Select(x => x.SaldoConta).FirstOrDefault();

            return saldo;
        }

        public OperacaoDTO Sacar(ContaDTO conta, bool isTransferencia, decimal valorSacar)
        {
            OperacaoDTO operacao = new OperacaoDTO();

            var caixaEletronico = _commonDbContext.Caixas.FirstOrDefault();

            var contaUsuario = _commonDbContext.Contas.Where(x => x.Banco == conta.BancoContaCli && x.Agencia == conta.AgenciaContaCli && x.NumeroContaCli == conta.NumeroContaCli && x.CpfCliente == conta.CpfCli).FirstOrDefault();

            if (_caixaEletronicoDAO.ValidarSaque(valorSacar, contaUsuario, caixaEletronico))
            {
                if (isTransferencia)
                {
                    contaUsuario.SaldoConta -= valorSacar;
                    conta.SaldoConta = contaUsuario.SaldoConta;

                    _commonDbContext.Update(contaUsuario);
                    _commonDbContext.Update(caixaEletronico);
                    _commonDbContext.SaveChanges();

                    operacao.Conta = conta;
                    operacao.Realizada = true;

                    return operacao;
                }
                else
                {
                    var notasParaSaque = _caixaEletronicoDAO.RetornarNotasNecessarias(valorSacar);

                    caixaEletronico.NotasCem -= notasParaSaque[0];
                    caixaEletronico.NotasCinquenta -= notasParaSaque[1];
                    caixaEletronico.NotasVinte -= notasParaSaque[2];
                    caixaEletronico.NotasCem -= notasParaSaque[3];
                    caixaEletronico.Valor_Disponivel -= valorSacar;

                    contaUsuario.SaldoConta -= valorSacar;
                    conta.SaldoConta = contaUsuario.SaldoConta;

                    _commonDbContext.Update(contaUsuario);
                    _commonDbContext.Update(caixaEletronico);
                    _commonDbContext.SaveChanges();

                    operacao.NotasUtilizadas = new string[4] { "R$ 100: " + notasParaSaque[0], "R$ 50: " + notasParaSaque[1], "R$ 20: " + notasParaSaque[2], "R$ 10: " + notasParaSaque[3] };
                    operacao.ValorSacado = valorSacar;
                    operacao.Conta = conta;
                    operacao.Realizada = true;

                    return operacao;
                }
            }
            return operacao;
        }

        public bool Depositar(ContaDTO conta, bool outraConta, decimal ValorDepositar)
        {
            if (outraConta)
            {
                var contaDestino = _commonDbContext.Contas.Where(x => x.Banco == conta.BancoContaCli && x.Agencia == conta.AgenciaContaCli && x.NumeroContaCli == conta.NumeroContaCli && x.CpfCliente == conta.CpfCli).FirstOrDefault();

                if (contaDestino == null)
                {
                    return false;
                }

                contaDestino.SaldoConta += ValorDepositar;

                _commonDbContext.Update(contaDestino);

                _commonDbContext.SaveChanges();

                return true;
            }
            else
            {
                var contaUsario = _commonDbContext.Contas.Where(x => x.Banco == conta.BancoContaCli && x.Agencia == conta.AgenciaContaCli && x.NumeroContaCli == conta.NumeroContaCli && x.CpfCliente == conta.CpfCli).FirstOrDefault();

                contaUsario.SaldoConta += ValorDepositar;

                _commonDbContext.Update(contaUsario);

                _commonDbContext.SaveChanges();

                return true;
            }

        }

        public bool Transferir(ContasTransferenciaDTO contasTransferencia, decimal ValorTransferir)
        {
            if (_caixaEletronicoDAO.ValidarInformacoes(contasTransferencia))
            {
                ContaDTO contaUsuario = new ContaDTO()
                {
                    AgenciaContaCli = contasTransferencia.Usuario_Agencia,
                    BancoContaCli = contasTransferencia.Usuario_Banco,
                    CpfCli = contasTransferencia.Usuario_Cpf,
                    NumeroContaCli = contasTransferencia.Usuario_NumeroConta
                };

                ContaDTO contaDestino = new ContaDTO()
                {
                    AgenciaContaCli = contasTransferencia.Destino_Agencia,
                    BancoContaCli = contasTransferencia.Destino_Banco,
                    CpfCli = contasTransferencia.Destino_Cpf,
                    NumeroContaCli = contasTransferencia.Destino_NumeroConta
                };

                this.Sacar(contaUsuario, true, ValorTransferir);
                this.Depositar(contaDestino, true, ValorTransferir);
                return true;
            }
            return false;
        }
    }
}