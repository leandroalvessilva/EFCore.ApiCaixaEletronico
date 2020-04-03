using ApiCaixaEletronico.DAO.Interface;
using ApiCaixaEletronico.DTO.Context;
using ApiCaixaEletronico.DTO.DTO;
using System;
using System.Linq;

namespace ApiCaixaEletronico.DAO.DAO
{
    public class CaixaEletronicoDAO : ICaixaEletronicoDAO
    {
        private CommonDbContext _commonDbContext;

        public CaixaEletronicoDAO(CommonDbContext commonDbContext)
        {
            this._commonDbContext = commonDbContext;
        }

        public int[] ConsultarNotasDisponiveis()
        {
            var caixaEletronico = _commonDbContext.Caixas.FirstOrDefault();

            int[] notasDisponíveis = new int[4] { caixaEletronico.NotasCem, caixaEletronico.NotasCinquenta, caixaEletronico.NotasVinte, caixaEletronico.NotasDez };

            return notasDisponíveis;
        }

        public int[] CalcularNotasNecessarias(int[] notasNecessarias)
        {
            int[] notasDisponives = this.ConsultarNotasDisponiveis();

            int[] notas = new int[4];

            for (int i = 0; i <= 3; i++)
            {
                if (notasDisponives[i] < notasNecessarias[i])
                {
                    int adicionarNota;

                    notas[i] = notasDisponives[i];

                    if (i == 0) //R$100
                    {
                        adicionarNota = notasNecessarias[i] - notasDisponives[i];

                        notasNecessarias[1] += (2 * adicionarNota);
                    }
                    else if (i == 1) //R$50
                    {
                        adicionarNota = notasNecessarias[i] - notasDisponives[i];

                        if (adicionarNota % 2 == 0)
                        {
                            var notasDeVinte = (adicionarNota * 50) / 20;
                            notasNecessarias[2] += notasDeVinte;
                        }
                        else
                        {
                            notasNecessarias[2] += (2 * adicionarNota + 1);
                            notasNecessarias[3] += (1 * adicionarNota % 2);
                        }
                    }
                    else if (i == 2) //R$20
                    {
                        adicionarNota = notasNecessarias[i] - notasDisponives[i];

                        notasNecessarias[3] += (2 * adicionarNota);
                    }
                    else
                    {
                        throw new Exception("Valor para saque maior que o número de notas disponível em caixa.");
                    }
                }
                else
                {
                    notas[i] += notasNecessarias[i];
                }
            }
            return notas;
        }

        public int[] RetornarNotasNecessarias(decimal ValorSacar)
        {
            int[] notasAceitas = new int[] { 100, 50, 20, 10 };

            int[] notasUtilizadas = new int[4];

            for (int i = 0; i <= 4; i++)
            {
                decimal result;

                if (ValorSacar == 0)
                {
                    notasUtilizadas = this.CalcularNotasNecessarias(notasUtilizadas);

                    return notasUtilizadas;
                }
                else
                {
                    result = ValorSacar / notasAceitas[i];

                    if (result % 1 != 0)
                    {
                        result = Math.Floor(result);
                    }

                    ValorSacar -= result * notasAceitas[i];

                    notasUtilizadas[i] = Convert.ToInt32(result);
                }
            }
            return notasUtilizadas;
        }

        public bool ValidarSaque(decimal valorSacar, ContaContext contaUsuario, CaixaEletronicoContext caixaEletronico)
        {
            if (contaUsuario.SaldoConta < valorSacar)
            {
                throw new Exception("valor para saque maior que o saldo disponível em conta.");
            }
            else if (valorSacar % 10 != 0 || valorSacar == 0 || valorSacar > 2000)
            {
                return false;
            }
            else if (caixaEletronico.Valor_Disponivel < valorSacar)
            {
                throw new Exception("valor para saque maior que o saldo disponível em caixa.");
            }
            return true;
        }

        public bool ValidarInformacoes(ContasTransferenciaDTO contasTransferencia)
        {
            var contaUsario = _commonDbContext.Contas.Where(x => x.Banco == contasTransferencia.Usuario_Banco && x.Agencia == contasTransferencia.Usuario_Agencia && x.NumeroContaCli == contasTransferencia.Usuario_NumeroConta && x.CpfCliente == contasTransferencia.Usuario_Cpf).FirstOrDefault();

            var contaDestinataria = _commonDbContext.Contas.Where(x => x.Banco == contasTransferencia.Destino_Banco && x.Agencia == contasTransferencia.Destino_Agencia && x.NumeroContaCli == contasTransferencia.Destino_NumeroConta && x.CpfCliente == contasTransferencia.Destino_Cpf).FirstOrDefault();

            if (contaUsario == null || contaDestinataria == null)
            {
                return false;
            }
            return true;
        }

        public bool ValidarDeposito(ContaContext conta, decimal valorDepositar, string[] notasDepositadas)
        {
            if (conta == null)
            {
                throw new Exception("Conta não consta em nosso banco de dados.Aguarde alguns minutos e tente novamente.");
            }
            else if (valorDepositar == 0 || valorDepositar > 5000)
            {
                return false;
            }
            else if (this.ConsultarNotasDisponiveis() != null)
            {
                var notasDisponives = this.ConsultarNotasDisponiveis();

                notasDisponives[0] += Convert.ToInt32(notasDepositadas[0]);
                notasDisponives[1] += Convert.ToInt32(notasDepositadas[1]);
                notasDisponives[2] += Convert.ToInt32(notasDepositadas[2]);
                notasDisponives[3] += Convert.ToInt32(notasDepositadas[3]);

                if (notasDisponives[0] > 100 || notasDisponives[1] > 100 || notasDisponives[2] > 100
                        || notasDisponives[3] > 100)
                {
                    throw new Exception("valor para depósito excede quantidade de notas máximas permitidas no caixa.");
                }
            }
            return true;
        }

        public bool Login(long cpf, int senha)
        {
            var contaUsario = _commonDbContext.Contas.Where(x => x.CpfCliente == cpf && x.SenhaConta == senha).FirstOrDefault();

            if (contaUsario == null)
            {
                return false;
            }
            return true;
        }

        public ContaDTO ListarUsuario(long cpf)
        {
            ContaDTO conta = new ContaDTO();

            var clienteBanco = _commonDbContext.Clientes.Where(x => x.CpfCli == cpf).FirstOrDefault();
            var contaUsario = _commonDbContext.Contas.Where(x => x.CpfCliente == cpf).FirstOrDefault();

            conta.AgenciaContaCli = contaUsario.Agencia;
            conta.BancoContaCli = contaUsario.Banco;
            conta.NumeroContaCli = contaUsario.NumeroContaCli;
            conta.SaldoConta = contaUsario.SaldoConta.ToString().Length < 12 ? Convert.ToDecimal(contaUsario.SaldoConta.ToString().PadRight(12, '0')) : contaUsario.SaldoConta;
            conta.Nome_Cliente = clienteBanco.Nome_Cliente.Length < 40 ? clienteBanco.Nome_Cliente.PadRight(40, ' ') : clienteBanco.Nome_Cliente;
            conta.CpfCli = contaUsario.CpfCliente;

            return conta;
        }
    }
}
