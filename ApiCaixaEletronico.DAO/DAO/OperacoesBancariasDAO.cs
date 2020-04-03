using ApiCaixaEletronico.DAO.Interface;
using ApiCaixaEletronico.DTO.DTO;
using System;
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

        public OperacaoDTO Sacar(ContaDTO conta, decimal valorSacar)
        {
            OperacaoDTO operacao = new OperacaoDTO();

            var caixaEletronico = _commonDbContext.Caixas.FirstOrDefault();

            var contaUsuario = _commonDbContext.Contas.Where(x => x.Banco == conta.BancoContaCli && x.Agencia == conta.AgenciaContaCli && x.NumeroContaCli == conta.NumeroContaCli && x.CpfCliente == conta.CpfCli).FirstOrDefault();

            if (_caixaEletronicoDAO.ValidarSaque(valorSacar, contaUsuario, caixaEletronico))
            {
                var notasParaSaque = _caixaEletronicoDAO.RetornarNotasNecessarias(valorSacar);

                caixaEletronico.NotasCem -= notasParaSaque[0];
                caixaEletronico.NotasCinquenta -= notasParaSaque[1];
                caixaEletronico.NotasVinte -= notasParaSaque[2];
                caixaEletronico.NotasDez -= notasParaSaque[3];
                caixaEletronico.Valor_Disponivel -= valorSacar;

                contaUsuario.SaldoConta -= valorSacar;
                conta.SaldoConta = contaUsuario.SaldoConta.ToString().Length < 12 ? Convert.ToDecimal(contaUsuario.SaldoConta.ToString().PadRight(12, '0')) : contaUsuario.SaldoConta;

                _commonDbContext.Update(contaUsuario);
                _commonDbContext.Update(caixaEletronico);
                _commonDbContext.SaveChanges();

                operacao.NotasUtilizadas = new int[4] { notasParaSaque[0], notasParaSaque[1], notasParaSaque[2], notasParaSaque[3] };
                operacao.ValorSacado = valorSacar;
                operacao.Conta = conta;
                operacao.Realizada = true;

                return operacao;
            }
            return operacao;
        }

        public bool Depositar(ContaDTO conta, decimal valorDepositar, string notasDepositadas)
        {
            var contaUsario = _commonDbContext.Contas.Where(x => x.Banco == conta.BancoContaCli && x.Agencia == conta.AgenciaContaCli && x.NumeroContaCli == conta.NumeroContaCli && x.CpfCliente == conta.CpfCli).FirstOrDefault();

            var caixaEletronico = _commonDbContext.Caixas.FirstOrDefault();

            var notasDeposito = notasDepositadas.Split(',');

            if (!_caixaEletronicoDAO.ValidarDeposito(contaUsario, valorDepositar, notasDeposito))
            {
                return false;
            }

            caixaEletronico.NotasCem += Convert.ToInt32(notasDeposito[0]);
            caixaEletronico.NotasCinquenta += Convert.ToInt32(notasDeposito[1]);
            caixaEletronico.NotasVinte += Convert.ToInt32(notasDeposito[2]);
            caixaEletronico.NotasDez += Convert.ToInt32(notasDeposito[3]);
            caixaEletronico.Valor_Disponivel += valorDepositar;

            contaUsario.SaldoConta += valorDepositar;

            _commonDbContext.Update(contaUsario);
            _commonDbContext.Update(caixaEletronico);

            _commonDbContext.SaveChanges();

            return true;
        }
    }
}