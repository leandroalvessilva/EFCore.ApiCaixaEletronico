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

        public Retorno Saldo(int banco, int agencia, int numeroConta, long cpf)
        {
            try
            {
                var result = _operacoesDao.Saldo(banco, agencia, numeroConta, cpf);

                return new Retorno()
                {
                    Codigo = 200,
                    Data = JsonConvert.SerializeObject(result),
                    Mensagem = "Consulta efetuada com sucesso"
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

        public Retorno Sacar(ContaDTO conta, decimal valorSacar)
        {
            try
            {
                var result = _operacoesDao.Sacar(conta, valorSacar);

                if (result.Realizada)
                {
                    return new Retorno()
                    {
                        Codigo = 200,
                        Mensagem = "Saque realizado com sucesso.",
                        Data = JsonConvert.SerializeObject(result)
                    };
                }
                return new Retorno()
                {
                    Codigo = 500,
                    Mensagem = "Não há notas disponíveis para realizar este saque."
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

        public Retorno Depositar(ContaDTO conta, decimal valorDepositar, string notasDepositadas)
        {
            try
            {
                var result = _operacoesDao.Depositar(conta, valorDepositar, notasDepositadas);

                if (result)
                {
                    return new Retorno()
                    {
                        Codigo = 200,
                        Mensagem = "Depósito realizado com sucesso.",
                        Data = JsonConvert.SerializeObject(result)
                    };
                }
                return new Retorno()
                {
                    Codigo = 500,
                    Mensagem = "O valor para depósito não pode ser zero e deve ser menor que o limite definido pelo banco de R$5.000,00."
                };
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
    }
}
