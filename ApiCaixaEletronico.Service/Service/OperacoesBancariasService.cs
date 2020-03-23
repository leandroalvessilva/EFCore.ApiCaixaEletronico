using ApiCaixaEletronico.DAO.DAO;
using ApiCaixaEletronico.DTO;
using ApiCaixaEletronico.DTO.DTO;
using ApiCaixaEletronico.Service.Interface;
using Newtonsoft.Json;
using System;

namespace ApiCaixaEletronico.Service.Service
{
    public class OperacoesBancariasService : IOperacoesBancariasService
    {
        private readonly IOperacoesBancariasDAO _operacoesDao;

        public OperacoesBancariasService(IOperacoesBancariasDAO operacoesDao)
        {
            this._operacoesDao = operacoesDao;
        }

        public Retorno Saldo(ContaDTO conta)
        {
            try
            {
                var result = _operacoesDao.Saldo(conta);

                return new Retorno()
                {
                    Codigo = 200,
                    Data = JsonConvert.SerializeObject(result),
                    Mensagem = "Consultado efetuada com sucesso"
                };
            }

            catch (Exception ex)
            {
                return new Retorno()
                {
                    Codigo = 500,
                    Mensagem = "Erro ao realizar consultar do saldo: " + ex.Message
                };
            }
        }

        public Retorno Sacar(ContaDTO conta, bool isTransferencia, decimal valorSacar)
        {
            try
            {
                var result = _operacoesDao.Sacar(conta, isTransferencia, valorSacar);

                if (result.Realizada == false)
                {
                    return new Retorno()
                    {
                        Codigo = 500,
                        Mensagem = "Erro ao realizar operação, verifique o saldo e o valor digitado para saque."
                    };
                }

                return new Retorno()
                {
                    Codigo = 200,
                    Mensagem = "Saque realizado com sucesso.",
                    Data = JsonConvert.SerializeObject(result)
                };
            }
            catch (Exception ex)
            {
                return new Retorno()
                {
                    Codigo = 500,
                    Mensagem = "Erro ao realizar saque: " + ex.Message
                };
            }
        }

        public Retorno Depositar(ContaDTO conta, bool outraConta, decimal ValorDepositar)
        {
            try
            {
                var result = _operacoesDao.Depositar(conta, outraConta, ValorDepositar);

                if (result == false)
                {
                    return new Retorno()
                    {
                        Codigo = 500,
                        Mensagem = "Depósito não realizado, verique as informações da conta."
                    };
                }
                else
                {
                    return new Retorno()
                    {
                        Codigo = 200,
                        Mensagem = "Depósito realizado com sucesso."
                    };
                }
            }

            catch (Exception ex)
            {
                return new Retorno()
                {
                    Codigo = 500,
                    Mensagem = "Erro ao realizar depósito: " + ex.Message
                };
            }
        }

        public Retorno Transferir(ContaDTO conta, ContaDTO contaDestino, decimal ValorTransferir)
        {
            try
            {
                var result = _operacoesDao.Transferir(conta, contaDestino, ValorTransferir);

                if (result == false)
                {
                    return new Retorno()
                    {
                        Codigo = 500,
                        Mensagem = "Erro ao realizar transferência, verifique as informações."
                    };
                }
                else
                {
                    return new Retorno()
                    {
                        Codigo = 200,
                        Mensagem = "Transferência realizada com Sucesso."
                    };
                }
            }

            catch (Exception ex)
            {
                return new Retorno()
                {
                    Codigo = 500,
                    Mensagem = "Erro ao realizar transferência: " + ex.Message
                };
            }
        }
    }
}
