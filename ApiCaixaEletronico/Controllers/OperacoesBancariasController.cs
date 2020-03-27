using ApiCaixaEletronico.DTO;
using ApiCaixaEletronico.DTO.DTO;
using ApiCaixaEletronico.Service.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ApiCaixaEletronico.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("AllowSpecificOrigin")]
    public class OperacoesBancariasController : Controller
    {
        private readonly IOperacoesBancariasService _operacoesService;

        public OperacoesBancariasController(IOperacoesBancariasService operacoesService)
        {
            this._operacoesService = operacoesService;
        }
               
        [HttpGet]
        [Route("Saldo")]
        public ActionResult Saldo(int banco, int agencia, int numeroConta, long cpf)
        {
            try
            {
                var saldo = _operacoesService.Saldo(banco,agencia,numeroConta,cpf);

                return Ok(saldo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Retorno()
                {
                    Codigo = 500,
                    Data = null,
                    Mensagem = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("Sacar")]
        public ActionResult Sacar(ContaDTO conta, bool isTransferencia, decimal ValorSacar)
            {
            try
            {
                var result = _operacoesService.Sacar(conta, isTransferencia, ValorSacar);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Retorno()
                {
                    Codigo = 500,
                    Mensagem = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("Depositar")]
        public ActionResult Depositar(ContaDTO conta, bool outraConta, decimal ValorDepositar)
        {
            try
            {
                var result = _operacoesService.Depositar(conta, outraConta, ValorDepositar);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Retorno()
                {
                    Codigo = 500,
                    Mensagem = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("Transferir")]
        public ActionResult Transferir(ContasTransferenciaDTO contasTransferencia, decimal ValorTransferir)
        {
            try
            {
                var result = _operacoesService.Transferir(contasTransferencia, ValorTransferir);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Retorno()
                {
                    Codigo = 500,
                    Mensagem = ex.Message
                });
            }
        }
    }
}
